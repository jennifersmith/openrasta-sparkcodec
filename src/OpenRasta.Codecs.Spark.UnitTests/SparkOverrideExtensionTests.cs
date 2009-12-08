using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using OpenRasta.Codecs.Spark2.Transformers;
using Rhino.Mocks;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkOverrideExtensionTests
	{
		[Test]
		public void NodeShouldBeTakenFromTransformer()
		{
			var elementTransformer = MockRepository.GenerateStub<ISparkElementTransformer>();
			var transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			IList<Node> body = new List<Node>();
			var extension = new SparkOverrideExtension(transformedNode,  elementTransformer);

			extension.VisitNode(new StubNodeVisitor(), body, null);

			elementTransformer.AssertWasCalled(x => x.Transform(Arg<SparkElementWrapper>.Is.Equal(new SparkElementWrapper(transformedNode,body))));
		}
		[Test]
		public void ShouldVisitTransformedNodeOnlyIfItIsEmptyAfterTransformation()
		{
			var elementTransformer = new StubSparkElementTransformer().WithRemoveAllBodyNodesAction();
			ElementNode transformedNode = SparkTestNodes.ElementNode("foo");
			var extension = new SparkOverrideExtension( transformedNode, elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new List<Node>(){new TextNode("hi")}, null);

			stubNodeVisitor.Nodes.ShouldOnlyContain(transformedNode);
		}
		[Test]
		public void ShouldSetElementEmptyIfNoBodyLeftAfterTransform()
		{
			var elementTransformer = new StubSparkElementTransformer().WithRemoveAllBodyNodesAction();
			ElementNode transformedNode = SparkTestNodes.ElementNode("foo");
			var extension = new SparkOverrideExtension(transformedNode, elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new List<Node>() { new TextNode("hi") }, null);

			Assert.That(transformedNode.IsEmptyElement, Is.True);
		}
		[Test]
		public void ShouldVisitTransformedNodeBodyIfNotEmpty()
		{
			ElementNode transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			List<Node> body = new List<Node>() { new TextNode("Body text"), new TextNode("Another one") };
			var elementTransformer = new StubSparkElementTransformer().WithAddNewBodyNodeAction(body);
			var extension = new SparkOverrideExtension(transformedNode, elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new Node[0], null);

			List<Node> expectedNodes = new List<Node> { transformedNode };
			expectedNodes.AddRange(body);
			stubNodeVisitor.Nodes.ShouldStartWith(expectedNodes);
		}
		[Test]
		public void ShouldAppendEndElementIfNodeIsNotEmpty()
		{
			ElementNode transformedNode = new ElementNode("transformed", new List<AttributeNode>(), true);
			var elementTransformer = new StubSparkElementTransformer().WithAddNewBodyNodeAction();
			var extension = new SparkOverrideExtension(transformedNode, elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new List<Node>(), null);

			stubNodeVisitor.Nodes.LastOrDefault().As<EndElementNode>().Name.ShouldEqual("transformed");
		}

		[Test]
		public void ShouldSetIsEmptyFalseIfNotEmpty()
		{
			ElementNode transformedNode = new ElementNode("transformed", new List<AttributeNode>(), true);
			var elementTransformer = new StubSparkElementTransformer().WithAddNewBodyNodeAction();
			var extension = new SparkOverrideExtension(transformedNode, elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new List<Node>(), null);

			stubNodeVisitor.Nodes.LastOrDefault().As<EndElementNode>().Name.ShouldEqual("transformed");
			transformedNode.IsEmptyElement.ShouldBeFalse();
		}
		[Test]
		public void ShouldVisitTransformedNodeAndBody()
		{
			var elementTransformer = MockRepository.GenerateStub<ISparkElementTransformer>();
			var nodeVisitor = MockRepository.GenerateStub<INodeVisitor>();
			IList<Node> body = new List<Node>(){new TextNode("Fred")};
			ElementNode transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			elementTransformer.Stub(x => x.Transform(new SparkElementWrapper(transformedNode, new Node[0])));
			var extension = new SparkOverrideExtension(transformedNode, elementTransformer);

			extension.VisitNode(nodeVisitor, body, null);

			nodeVisitor.AssertWasCalled(x => x.Accept(transformedNode));
			nodeVisitor.AssertWasCalled(x => x.Accept(Arg<IList<Node>>.Is.Equal(body)));
		}
	}

	public class StubSparkElementTransformer : ISparkElementTransformer
	{
		private List<Action<IElement>> _action = new List<Action<IElement>>();
		public void Transform(IElement element)
		{
			_action.ForEach(x=>x(element));
		}
		public  StubSparkElementTransformer WithAction(Action<IElement> action )
		{
			_action.Add(action);
			return this;
		}
		public StubSparkElementTransformer WithRemoveAllBodyNodesAction()
		{
			return WithAction( x => x.As<SparkElementWrapper>().Body.Clear()).WithSetNodeEmptyAction();
		}

		private StubSparkElementTransformer WithSetNodeEmptyAction()
		{
			return WithAction(x => x.As<SparkElementWrapper>().CurrentNode.IsEmptyElement = true);
		}

		private StubSparkElementTransformer WithSetNodeNonEmptyAction()
		{
			return WithAction(x => x.As<SparkElementWrapper>().CurrentNode.IsEmptyElement = false);
		}

		public StubSparkElementTransformer WithAddNewBodyNodeAction()
		{
			return WithAction(x => x.As<SparkElementWrapper>().Body.Add(new TextNode("Hello"))).WithSetNodeNonEmptyAction();
		}

		public ISparkElementTransformer WithAddNewBodyNodeAction(IEnumerable<Node> nodes)
		{
			return WithAction(x => nodes.ForEach(y=> x.As<SparkElementWrapper>().Body.Add(y))).WithSetNodeNonEmptyAction();
		}
	}

	public class StubNodeVisitor : INodeVisitor
	{
		private List<Node> _nodes = new List<Node>();

		public void Accept(IList<Node> nodes)
		{
			_nodes.AddRange(nodes);
		}

		public void Accept(Node node)
		{
			_nodes.Add(node);
		}

		public VisitorContext Context
		{
			get; set;
		}

		public IList<Node> Nodes
		{
			get
			{
				return _nodes;
			}
		}

	}

}