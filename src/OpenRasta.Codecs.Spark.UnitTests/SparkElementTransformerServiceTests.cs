using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using OpenRasta.Codecs.Spark2.Transformers;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class SparkElementTransformerServiceTests
	{
		private StubElementTransformer _elementTransformer;
		private SparkElementTransformerService _target;

		[SetUp]
		public void SetUp()
		{
			SparkElementNode = null;
			_elementTransformer = new StubElementTransformer();
			_target = new SparkElementTransformerService(_elementTransformer);
		}
		public ElementNode SparkElementNode { get; set; }
		public ISparkElementTransformer ReturnedElementTransformer { get; set; }

		[Test]
		public void ShouldReturnNoElementTransformInstanceIfElementIsNotTransformble()
		{
			WhenWeAskForAnElementTransformer();

			ThenReturnedElementTransformerShouldBeNullTransform();
		}

		private void ThenReturnedElementTransformerShouldBeNullTransform()
		{
			ReturnedElementTransformer.As<NullSparkElementTransformer>();
		}

		private void ThenReturnedElementTransformerShouldWrap(NoElementTransform noElementTransform)
		{
			ReturnedElementTransformer.As<SparkElementTransformer>().WrappedTransformer.ShouldEqual(noElementTransform);
		}

		private void WhenWeAskForAnElementTransformer()
		{
			ReturnedElementTransformer = _target.CreateElementTransformer(SparkElementNode);
		}

		private void GivenATransformableSparkElementNode(IElementTransformer elementTransformer)
		{
			SparkElementNode = new ElementNode("transformable", new List<AttributeNode>(), true);
			_elementTransformer.WithAvailableTransform(new SparkElementWrapper(SparkElementNode), elementTransformer);
		}
	}


	
	public class StubElementTransformer : IElementTransformerService
	{
		Dictionary<IElement, IElementTransformer> _transforms = new Dictionary<IElement, IElementTransformer>();

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
}