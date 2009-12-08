using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkWrapperFactoryTests
	{
		public SparkWrapperFactoryTestContext Context
		{
			get; set;
		}
		[SetUp]
		public void SetUp()
		{
			Context = new SparkWrapperFactoryTestContext();
		}
		[Test]
		public void ShouldRecogniseAndWrapElementNodes()
		{
			GivenASparkNode(SparkTestNodes.BasicElementNode());
			WhenTheNodeIsWrapped();
			ThenTheWrappedNodeShouldBe<SparkElementWrapper>();
			AndTheWrappedNodeShouldWrapTheOriginalNode();
		}
		[Test]
		public void ShouldRecogniseAndWrapAttributeNodes()
		{
			GivenASparkNode(SparkTestNodes.BasicAttributeNode("attributeName"));
			WhenTheNodeIsWrapped();
			ThenTheWrappedNodeShouldBe<SparkAttributeWrapper>();
			AndTheWrappedNodeShouldWrapTheOriginalNode();
		}
		[Test]
		public void ShouldMapUnknownNodesToUnknownNodeType()
		{
			GivenASparkNode(SparkTestNodes.UnknownNode());
			WhenTheNodeIsWrapped();
			ThenTheWrappedNodeShouldBe<UnrecognisedSparkNodeWrapper>();
			AndTheWrappedNodeShouldWrapTheOriginalNode();
		}

		private void AndTheWrappedNodeShouldWrapTheOriginalNode()
		{
			Assert.That(Context.WrappedNode.As<ISparkNodeWrapper>().GetWrappedNode(), Is.EqualTo(Context.NodeToWrap));
		}

		private void ThenTheWrappedNodeShouldBe<TWrapper>()
		{
			Assert.That(Context.WrappedNode, Is.InstanceOf<TWrapper>());
		}

		private void WhenTheNodeIsWrapped()
		{
			Context.WrappedNode = SparkWrapperFactory.CreateWrapper(Context.NodeToWrap);
		}

		private void GivenASparkNode(Node node)
		{
			this.Context.NodeToWrap = node;
		}

		public class SparkWrapperFactoryTestContext
		{
			public INode WrappedNode
			{
				get;
				set;
			}
			public Node NodeToWrap
			{
				get;
				set;
			}
		}
	}
}