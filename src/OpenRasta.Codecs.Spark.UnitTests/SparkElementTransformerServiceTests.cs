using System;
using System.Collections.Generic;
using System.Linq;
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
		[Test]
		public void ShouldRetreiveTransformationsForATagIfHasMatchingAttributes()
		{
			var transformer = new StubElementTransformer();
			GivenAnElementNode(SparkTestNodes.ElementNode("foo").WithAttribute("bar", "wibble"));
			GivenATransformForATag(transformer, new Tag("foo", new TagAttribute("bar", "wibble")));
			WhenWeAskForAnElementTransformer();
			ThenReturnedElementTransformerShouldWrap(transformer);
		}
		[Test]
		public void ShouldNotRetreiveTransformationsForATagIfNotMatchingAttributes()
		{
			var transformer = new StubElementTransformer();
			GivenAnElementNode(SparkTestNodes.ElementNode("foo").WithAttribute("bar", "wibble"));
			GivenATransformForATag(transformer, new Tag("foo", new TagAttribute("bar", "bah")));
			WhenWeAskForAnElementTransformer();
			ThenReturnedElementTransformerShouldBeNullTransform();
		}

		private void GivenAnElementNode(ElementNode node)
		{
			Context.SparkElementNode = node;
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
		private void GivenATransformForATag(IElementTransformer transformer, Tag tag)
		{
			Context.ElementTransformerService.WithAvailableTransform(tag, transformer);

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

		public IElementTransformer GetTransformerFor(Tag tag)
		{
			return _transformsByTag.Where(x => x.Key.Matches(tag)).Select(x => x.Value).FirstOrDefault();
		}

		public IElementTransformer GetTransformerFor(IElement element)
		{
			throw new NotImplementedException();
		}

		public bool IsTransformable(IElement element)
		{
			throw new NotImplementedException();
		}

		public bool IsTransformable(Tag tag)
		{
			return GetTransformerFor(tag) != null;
		}
	}

	
}