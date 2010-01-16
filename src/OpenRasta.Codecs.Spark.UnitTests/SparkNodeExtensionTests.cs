using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkNodeExtensionTests
	{
		public NodeExtensionsContext Context { get; private set; }

		[SetUp]
		public void SetUp()
		{
			Context = new NodeExtensionsContext();
		}

		[Test]
		public void WrapAllShouldBeAbleToMapAWholeBunchOfNodes()
		{
			GivenNodesToWrap(SparkTestNodes.BasicAttributeNode("attributeName"), SparkTestNodes.BasicElementNode(),
			                 SparkTestNodes.BasicAttributeNode("attributeName"));

			WhenAllNodesAreMapped();
			TheResultShouldWrapTheOriginalSetOfNodes();
		}
		[Test]
		public void UnwrapShouldReturnUnderlyingSparkNode()
		{
			GivenNodeToUnwrap( InternalTestNodes.BasicElementNode());
			WhenUnwrappingTheNode();
			ThenTheResultShouldBeASparkElement();
		}

		[Test]
		public void HasNameShouldBeTrueIfElementHasName()
		{
			GivenNodeTarget(SparkTestNodes.ElementNode("fred"));
			ThenHasNameShouldBeTrueFor("fred");
		}
		[Test]
		public void HasNameShouldBeFalseIfElementDoesnotHaveName()
		{
			GivenNodeTarget(SparkTestNodes.ElementNode("barney"));
			ThenHasNameShouldBeFalseFor("fred");
		}

		[Test]
		public void HasNameShouldBeTrueIfHasNameButDifferentCase()
		{
			GivenNodeTarget(SparkTestNodes.ElementNode("FRED"));
			ThenHasNameShouldBeTrueFor("fred");
		}

		private void ThenHasNameShouldBeTrueFor(string name)
		{
			Context.NodeTarget.HasName(name).ShouldBeTrue();
		}
		private void ThenHasNameShouldBeFalseFor(string name)
		{
			Context.NodeTarget.HasName(name).ShouldBeFalse();
		}

		private void GivenNodeTarget(ElementNode node)
		{
			Context.NodeTarget = node;
		}

		private void ThenTheResultShouldBeASparkElement()
		{
			Context.UnwrappedNode.As<ElementNode>();
		}

		private void WhenUnwrappingTheNode()
		{
			Context.UnwrappedNode = Context.NodeToUnwrap.Unwrap();
		}

		private void GivenNodeToUnwrap(IElement element)
		{
			Context.NodeToUnwrap = element;
		}

		private void TheResultShouldWrapTheOriginalSetOfNodes()
		{
			var wrappedNodes = Context.WrappedNodes.Select(x => x.As<ISparkNodeWrapper>().GetWrappedNode());
			Assert.That(wrappedNodes, Is.EqualTo(Context.NodesToWrap));
		}

		private void WhenAllNodesAreMapped()
		{
			Context.WrappedNodes = Context.NodesToWrap.WrapAll();
		}

		private void GivenNodesToWrap(params Node[] nodes)
		{
			Context.NodesToWrap = nodes;
		}

		public class NodeExtensionsContext
		{
			public IEnumerable<INode> WrappedNodes
			{
				get;
				set;
			}
			public IEnumerable<Node> NodesToWrap
			{
				get;
				set;
			}

			public IElement NodeToUnwrap { get; set; }

			public Node UnwrappedNode { get; set; }

			public ElementNode NodeTarget { get; set; }

		}
	}
}