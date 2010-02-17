namespace OpenRasta.Codecs.Spark.UnitTests
{
	public class SparkRendererBuilder
	{
		private ISparkViewResolver _sparkViewResolver = new StubSparkViewResolver();

		public SparkRenderer Build()
		{
			return new SparkRenderer(_sparkViewResolver);
		}

		public SparkRendererBuilder With(ISparkViewResolver sparkViewResolver)
		{
			_sparkViewResolver = sparkViewResolver;
			return this;
		}
	}
}