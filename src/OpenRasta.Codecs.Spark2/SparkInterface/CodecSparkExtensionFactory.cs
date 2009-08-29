using Spark;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class CodecSparkExtensionFactory : ISparkExtensionFactory
	{
		private readonly ISparkElementTransformerService _sparkElementTransformerService;

		public CodecSparkExtensionFactory(ISparkElementTransformerService sparkElementTransformerService)
		{
			_sparkElementTransformerService = sparkElementTransformerService;
		}

		public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
		{
			ISparkElementTransformer elementTransformer = _sparkElementTransformerService.CreateElementTransformer(node);
			if(elementTransformer is NullSparkElementTransformer)
			{
				return null;
			}
			return new SparkOverrideExtension(elementTransformer);
		}
	}
}