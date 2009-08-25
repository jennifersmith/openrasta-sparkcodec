using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.SparkInterface;
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
			var elementTransformer = MockRepository.GenerateStub<IElementTransformer>();
			Node transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			IList<Node> body = new List<Node>();
			var extension = new SparkOverrideExtension(elementTransformer);

			extension.VisitNode(new StubNodeVisitor(), body, null);

			elementTransformer.AssertWasCalled(x => x.Transform(body));
		}
		[Test]
		public void ShouldVisitTransformedNodeOnlyIfItIsEmpty()
		{
			var elementTransformer = MockRepository.GenerateStub<IElementTransformer>();
			Node transformedNode = new ElementNode("transformed", new List<AttributeNode>(), true);
			elementTransformer.Stub(x => x.Transform(null)).IgnoreArguments().Return(transformedNode);
			var extension = new SparkOverrideExtension(elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new List<Node>(){new TextNode("Ignore this body text")}, null);

			stubNodeVisitor.Nodes.ShouldOnlyContain(transformedNode);

		}
		[Test]
		public void ShouldVisitTransformedNodeBodyIfNotEmpty()
		{
			var elementTransformer = MockRepository.GenerateStub<IElementTransformer>();
			Node transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			elementTransformer.Stub(x => x.Transform(null)).IgnoreArguments().Return(transformedNode);
			var extension = new SparkOverrideExtension(elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();
			List<Node> body = new List<Node>() {new TextNode("Body text"), new TextNode("Another one")};
			List<Node> expectedNodes = new List<Node> { transformedNode };
			expectedNodes.AddRange(body);

			extension.VisitNode(stubNodeVisitor, body, null);
			
			stubNodeVisitor.Nodes.ShouldStartWith(expectedNodes);
		}
		[Test]
		public void ShouldAppendEndElementIfNodeIsNotEmpty()
		{
			var elementTransformer = MockRepository.GenerateStub<IElementTransformer>();
			Node transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			elementTransformer.Stub(x => x.Transform(null)).IgnoreArguments().Return(transformedNode);
			var extension = new SparkOverrideExtension(elementTransformer);
			var stubNodeVisitor = new StubNodeVisitor();

			extension.VisitNode(stubNodeVisitor, new List<Node>(), null);

			stubNodeVisitor.Nodes.LastOrDefault().As<EndElementNode>().Name.ShouldEqual("transformed");
		}
		[Test]
		public void ShouldNotRet()
		{
			var elementTransformer = MockRepository.GenerateStub<IElementTransformer>();
			var nodeVisitor = MockRepository.GenerateStub<INodeVisitor>();
			IList<Node> body = new List<Node>();
			Node transformedNode = new ElementNode("transformed", new List<AttributeNode>(), false);
			elementTransformer.Stub(x => x.Transform(body)).Return(transformedNode);
			var extension = new SparkOverrideExtension(elementTransformer);

			extension.VisitNode(nodeVisitor, body, null);

			nodeVisitor.AssertWasCalled(x => x.Accept(transformedNode));
			nodeVisitor.AssertWasCalled(x => x.Accept(body));
		}
		
		//Transform(Node, body);

		//visitor.Accept(Node);
		//visitor.Accept(body);
		//if (!Node.IsEmptyElement)
		//{
		//    visitor.Accept(new EndElementNode(Node.Name));
		//}
		
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