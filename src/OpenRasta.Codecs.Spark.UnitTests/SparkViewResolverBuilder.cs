using Spark;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public class SparkViewResolverBuilder
	{
		private ISparkViewEngine _sparkViewEngine;

		public SparkViewResolver Build()
		{
			return new SparkViewResolver(_sparkViewEngine);
		}
		public SparkViewResolverBuilder With(ISparkViewEngine sparkViewEngine)
		{
			_sparkViewEngine = sparkViewEngine;
			return this;
		}
	}
}