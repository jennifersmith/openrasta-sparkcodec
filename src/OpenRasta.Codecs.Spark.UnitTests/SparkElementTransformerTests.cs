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
		public void ShouldPassTheElementToTheElementTransformerWhenTransforming()
		{
			IList<Node> innerNodes = new Node[]
			                               	{
			                               		SparkTestNodes.BasicAttributeNode("attributeName"),
												SparkTestNodes.BasicElementNode()
			                               	};
			SparkElementWrapper sparkElementWrapper = new SparkElementWrapper(SparkTestNodes.BasicElementNode(), innerNodes);
			GivenAnElementResultOf(sparkElementWrapper);
			GivenInnerNodesOf(innerNodes);
			WhenNodeIsTransformedWithElementAndBody(sparkElementWrapper);
			ThenTheElementTransformerShouldReceive(sparkElementWrapper);
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

		private void ThenTheElementTransformerShouldReceive(SparkElementWrapper elementWrapper)
		{
			Context.ElementTransformer.AssertWasCalled(x=>x.Transform(Arg<IElement>.Is.Equal(elementWrapper)));
		}

		private void WhenNodeIsTransformedWithElementAndBody(IElement element)
		{
			Context.Target.Transform(element);
		}

		public class SparkElementTransformerTestContext
		{
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
		public static TProperty GetSecondArgumentFor<T, TProperty>(this T target, Action<T> action)
		{
			object[] firstCallArguments = target.GetArgumentsForCallsMadeOn(action).FirstOrDefault();
			Assert.That(firstCallArguments, Is.Not.Null, "Method was not called");
			Assert.That(firstCallArguments.Length, Is.GreaterThanOrEqualTo(1), "Method has no parameters");
			return firstCallArguments.Skip(1).First().As<TProperty>();
		}
	}
}