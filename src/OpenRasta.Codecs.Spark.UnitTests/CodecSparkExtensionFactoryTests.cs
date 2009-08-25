using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Rhino.Mocks;
using Spark;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class CodecSparkExtensionFactoryTests
	{
		[Test]
		public void ShouldReturnNullIfNodeIsNotOverridable()
		{
			var node = new ElementNode("nonOverridableElement", new List<AttributeNode>(), false);
			IElementTransformerService elementTransformerService = CreateAlwaysFalseElementTransformerService(node);
			var sparkExtensionFactory = new CodecSparkExtensionFactory(elementTransformerService);

			ISparkExtension result = sparkExtensionFactory.CreateExtension(null, node);

			result.ShouldBeNull();
		}
		[Test]
		public void ShouldReturnElementTransformerIfNodeIsNotOverridable()
		{
			var node = new ElementNode("overridableElement", new List<AttributeNode>(), false);
			IElementTransformerService elementTransformerService = CreateAlwaysTrueElementTransformerService(node);
			var sparkExtensionFactory = new CodecSparkExtensionFactory(elementTransformerService);

			ISparkExtension result = sparkExtensionFactory.CreateExtension(null, node);

			result.As<SparkOverrideExtension>().ElementTransformer.As<StubElementTransformer>().ElementNode.ShouldEqual(node);
		}

		private static IElementTransformerService CreateAlwaysFalseElementTransformerService(ElementNode node)
		{
			IElementTransformerService elementTransformerService = CreateElementTransformerStub();
			elementTransformerService.Stub(x=>x.ElementIsOverridable(node)).Return(false);
			return elementTransformerService;
		}
		private static IElementTransformerService CreateAlwaysTrueElementTransformerService(ElementNode node)
		{
			IElementTransformerService elementTransformerService = CreateElementTransformerStub();
			elementTransformerService.Stub(x => x.ElementIsOverridable(node)).Return(true);
			return elementTransformerService;
		}

		private static IElementTransformerService CreateElementTransformerStub()
		{
			var elementTransformerStub = MockRepository.GenerateStub<IElementTransformerService>();
			elementTransformerStub.Stub(x => x.CreateElementTransformer(null)).IgnoreArguments().Do(new CreateElementTransformerDelegate(
			                                                                                        	node =>
			                                                                                        	new StubElementTransformer(
			                                                                                        		node)));
			return elementTransformerStub;
		}

		delegate IElementTransformer CreateElementTransformerDelegate(ElementNode node);

		private class StubElementTransformer : IElementTransformer
		{
			private readonly ElementNode _elementNode;

			public StubElementTransformer(ElementNode elementNode)
			{
				_elementNode = elementNode;
			}

			public ElementNode ElementNode
			{
				get { return _elementNode; }
			}

			public Node Transform(IList<Node> innerNodes)
			{
				throw new NotImplementedException();
			}
		}
	}
}