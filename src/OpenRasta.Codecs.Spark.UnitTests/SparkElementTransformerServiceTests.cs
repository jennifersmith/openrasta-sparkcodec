using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.UnitTests.Transformers;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using OpenRasta.Codecs.Spark2.Specification.Syntax;
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
			Context.SparkElementNode = SparkTestNodes.ElementNode("test");
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
			Context.ElementTransformerService.WithAvailableTransform(new Tag(Context.SparkElementNode.Name), elementTransformer);
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
		public IElement Transform(IElement element)
		{
			throw new NotImplementedException();
		}
	}
	
	public class StubElementTransformerService : IElementTransformerService
	{
		Dictionary<Tag, IElementTransformer> _transformsByTag = new Dictionary<Tag, IElementTransformer>();

		public void WithAvailableTransform(Tag tag, IElementTransformer transformer)
		{
			_transformsByTag[tag] = transformer;
		}

		public IElementTransformer GetTransformerFor(Tag element)
		{
			return _transformsByTag[element];
		}

		public IElementTransformer GetTransformerFor(IElement element)
		{
			throw new NotImplementedException();
		}

		public bool IsTransformable(IElement element)
		{
			throw new NotImplementedException();
		}

		public bool IsTransformable(Tag element)
		{
			return _transformsByTag.ContainsKey(element);
		}
	}

	
}