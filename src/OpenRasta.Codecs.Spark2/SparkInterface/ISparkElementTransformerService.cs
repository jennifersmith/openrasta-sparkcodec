using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Specification.Syntax;
using OpenRasta.Codecs.Spark2.Transformers;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public interface ISparkElementTransformerService
	{
		ISparkElementTransformer CreateElementTransformer(ElementNode elementNode);
	}


	public class SparkElementTransformerService : ISparkElementTransformerService
	{
		private readonly IElementTransformerService _elementTransformerService;

		public SparkElementTransformerService(IElementTransformerService elementTransformerService)
		{
			_elementTransformerService = elementTransformerService;
		}

		public ISparkElementTransformer CreateElementTransformer(ElementNode elementNode)
		{
			var tag = new Tag(elementNode.Name);
			if(!_elementTransformerService.IsTransformable(tag))
			{
				return new NullSparkElementTransformer();
			}
			return new SparkElementTransformer(_elementTransformerService.GetTransformerFor(tag));
		}
	}
}