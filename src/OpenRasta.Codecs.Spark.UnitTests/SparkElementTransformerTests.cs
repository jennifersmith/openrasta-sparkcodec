using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using OpenRasta.Codecs.Spark2.Transformers;
using Rhino.Mocks;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkElementTransformerTests
	{
		public SparkElementTransformerTestContext Context { get; set; }

		[SetUp]
		public void SetUp()
		{
			Context = new SparkElementTransformerTestContext();
			Context.ElementTransformer = MockRepository.GenerateStub<IElementTransformer>();
			Context.Target = new SparkElementTransformer(Context.ElementTransformer);
			Context.InnerNodes = new Node[0];
		}
		[Test]
		public void ShouldPassTheInnerNodesToTheElementTransformerWhenTransforming()
		{
			IList<Node> innerNodes = new Node[]
			                               	{
			                               		SparkTestNodes.BasicAttributeNode(),
												SparkTestNodes.BasicElementNode()
			                               	};
			GivenAnElementResultOf(new SparkElementWrapper(SparkTestNodes.BasicElementNode()));
			GivenInnerNodesOf(innerNodes);
			WhenNodeIsTransformedWithBody(innerNodes);
			ThenTheBodyNodesShouldBePassedIntoTheElementTransformer(innerNodes);
		}

		[Test]
		public void ShouldReturnUnwrappedElementNode()
		{
			GivenAnElementResultOf(new SparkElementWrapper(SparkTestNodes.BasicElementNode()));
			WhenNodeIsTransformed();
			ThenTheBodyResultShouldBeTheElementTransformerResultUnwrapped();
		}

		private void ThenTheBodyResultShouldBeTheElementTransformerResultUnwrapped()
		{
			Context.TransformationResult.ShouldEqual(Context.ElementTransformerResult.As<SparkElementWrapper>().WrappedNode);
		}

		private void WhenNodeIsTransformed()
		{
			WhenNodeIsTransformedWithBody(new Node[0]);
		}

		private void GivenAnElementResultOf(SparkElementWrapper elementWrapper)
		{
			Context.ElementTransformerResult = elementWrapper;
			Context.ElementTransformer.Stub(x => x.Transform(null)).IgnoreArguments().Return(elementWrapper);
		}

		private void GivenInnerNodesOf(IList<Node> nodes)
		{
			Context.InnerNodes = nodes;
		}

		private void ThenTheBodyNodesShouldBePassedIntoTheElementTransformer(IEnumerable<Node> nodes)
		{
			IEnumerable<INode> bodyNodes = Context.ElementTransformer.GetFirstArgumentFor<IElementTransformer, IEnumerable<INode>>(x => x.Transform(null));
			IEnumerable<Node> unwrappedNodes = bodyNodes.Cast<SparkNodeWrapper>().Select(x => x.WrappedNode);
			Assert.That(unwrappedNodes, Is.EqualTo(nodes));
		}

		private void WhenNodeIsTransformedWithBody(IList<Node> innerNodes)
		{
			Context.TransformationResult = Context.Target.Transform(innerNodes);
		}

		public class SparkElementTransformerTestContext
		{
			public Node TransformationResult { get; set; }

			public IElementTransformer ElementTransformer { get; set; }

			public SparkElementTransformer Target { get; set; }

			public IList<Node> InnerNodes { get; set; }

			public SparkElementWrapper ElementTransformerResult { get; set; }
		}
	}

	public static class RhinoExtensions
	{
		public static TProperty GetFirstArgumentFor<T,TProperty>(this T target, Action<T> action )
		{
			object[] firstCallArguments = target.GetArgumentsForCallsMadeOn(action).FirstOrDefault();
			Assert.That(firstCallArguments, Is.Not.Null, "Method was not called");
			Assert.That(firstCallArguments.Length, Is.GreaterThanOrEqualTo(1), "Method has no parameters");
			return firstCallArguments.First().As<TProperty>();
		}
	}
}