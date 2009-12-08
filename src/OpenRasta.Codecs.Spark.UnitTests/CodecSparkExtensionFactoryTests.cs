using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Rhino.Mocks;
using Spark;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class CodecSparkExtensionFactoryTests
	{
		[SetUp]
		public void SetUp()
		{
			_stubElementTransformerService = new StubSparkElementTransformerService();
			_target = new CodecSparkExtensionFactory(_stubElementTransformerService);
			ElementToUse = null;
			ResultSparkExtension = null;
			Transformer = new StubSparkElementTransformer();
		}

		private StubSparkElementTransformerService _stubElementTransformerService;
		private CodecSparkExtensionFactory _target;

		protected ElementNode ElementToUse { get; set; }
		protected StubSparkElementTransformer Transformer { get; set; }
		[Test]
		public void ShouldReturnNullIfNodeIsNotOverridable()
		{
			GivenANonOverridableElement();
			WhenWeAskForTheSparkExtension();
			ThenTheResultShouldBeNull();
		}
		[Test]
		public void ShouldReturnAValidExtensionIfIsOverridable()
		{
			GivenAnOverridableElement();
			WhenWeAskForTheSparkExtension();
			ThenTheResultShouldWrapTheGivenTransform();
		}

		private void ThenTheResultShouldWrapTheGivenTransform()
		{
			ResultSparkExtension.As<SparkOverrideExtension>().SparkElementTransformer.ShouldEqual(Transformer);
		}

		private void ThenTheResultShouldBeNull()
		{
			ResultSparkExtension.ShouldBeNull();
		}

		private void WhenWeAskForTheSparkExtension()
		{
			ResultSparkExtension = _target.CreateExtension(null, ElementToUse);
		}

		protected ISparkExtension ResultSparkExtension { get; set; }

		private void GivenANonOverridableElement()
		{
			ElementToUse = new ElementNode("nonOverridable", new List<AttributeNode>(), true);
		}

		private void GivenAnOverridableElement()
		{
			ElementToUse = new ElementNode("overridableElement", new List<AttributeNode>(), true);
			_stubElementTransformerService.WithTransformer(ElementToUse, Transformer);
		}

		
		protected  class StubSparkElementTransformer : ISparkElementTransformer
		{
			public void Transform(IElement element)
			{
				throw new NotImplementedException();
			}
		}
		protected class StubSparkElementTransformerService : ISparkElementTransformerService
		{
			private readonly Dictionary<ElementNode, ISparkElementTransformer> transfomersByElementNode = new Dictionary<ElementNode, ISparkElementTransformer>();

			public void WithTransformer(ElementNode node, ISparkElementTransformer sparkElementTransformer)
			{
				transfomersByElementNode[node] = sparkElementTransformer;
			}

			public ISparkElementTransformer CreateElementTransformer(ElementNode elementNode)
			{
				ISparkElementTransformer result;
				return transfomersByElementNode.TryGetValue(elementNode, out result) ? result : new NullSparkElementTransformer();
			}
		}
	}
}