using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.UnitTests.Transformers;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using OpenRasta.Codecs.Spark2.Transformers;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkElementTransformerServiceTests
	{

		[SetUp]
		public void SetUp()
		{
			Context = new SparkElementTransformerServiceTestContext
			          	{
			          		ElementTransformerService = new StubElementTransformerService()
			          	};
			Context.Target = new SparkElementTransformerService(Context.ElementTransformerService);
		}

		protected SparkElementTransformerServiceTestContext Context { get; set; }


		[Test]
		public void ShouldReturnNoElementTransformInstanceIfElementIsNotTransformble()
		{
			WhenWeAskForAnElementTransformer();

			ThenReturnedElementTransformerShouldBeNullTransform();
		}
		[Test]
		public void ShouldReturnCorrectElementTranformIsntanceIfElementIsTransformable()
		{
			var transformer = new StubElementTransformer();
			GivenATransformableSparkElementNode(transformer);
			WhenWeAskForAnElementTransformer();
			ThenReturnedElementTransformerShouldWrap(transformer);
		}

		private void ThenReturnedElementTransformerShouldBeNullTransform()
		{
			Context.ReturnedElementTransformer.As<NullSparkElementTransformer>();
		}

		private void ThenReturnedElementTransformerShouldWrap(IElementTransformer elementTransformer)
		{
			Context.ReturnedElementTransformer.As<SparkElementTransformer>().WrappedTransformer.ShouldEqual(elementTransformer);
		}

		private void WhenWeAskForAnElementTransformer()
		{
			Context.ReturnedElementTransformer = Context.Target.CreateElementTransformer(Context.SparkElementNode);
		}

		private void GivenATransformableSparkElementNode(IElementTransformer elementTransformer)
		{
			Context.SparkElementNode = new ElementNode("transformable", new List<AttributeNode>(), true);
			Context.ElementTransformerService.WithAvailableTransform(new SparkElementWrapper(Context.SparkElementNode), elementTransformer);
		}

		protected class SparkElementTransformerServiceTestContext
		{
			public StubElementTransformerService ElementTransformerService { get; set; }

			public SparkElementTransformerService Target { get; set; }

			public ISparkElementTransformer ReturnedElementTransformer { get; set; }

			public ElementNode SparkElementNode { get; set;}
		}
	}

	public class StubElementTransformer : IElementTransformer
	{
		public IElement Transform(IEnumerable<INode> body)
		{
			throw new NotImplementedException();
		}
	}
	
	public class StubElementTransformerService : IElementTransformerService
	{
		Dictionary<IElement, IElementTransformer> _transforms = new Dictionary<IElement, IElementTransformer>(new WrappedElementEqualityComparer());

		public void WithAvailableTransform(IElement node, IElementTransformer transformer)
		{
			_transforms[node] = transformer;
		}

		public IElementTransformer GetTransformerFor(IElement element)
		{
			return _transforms[element];
		}

		public bool IsTransformable(IElement element)
		{
			return _transforms.ContainsKey(element);
		}
	}

	internal class WrappedElementEqualityComparer : IEqualityComparer<IElement>
	{
		public bool Equals(IElement x, IElement y)
		{
			return x.Unwrap() == y.Unwrap();
		}

		public int GetHashCode(IElement obj)
		{
			return obj.Unwrap().GetHashCode();
		}
	}
}