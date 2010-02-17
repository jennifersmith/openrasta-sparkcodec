using Rhino.Mocks;
using Spark;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public static class SparkViewTestExtensions
	{
		public static ISparkViewEngine StubCreateInstance(this ISparkViewEngine sparkViewEngine)
		{
			return StubCreateInstance(sparkViewEngine, MockRepository.GenerateStub<ISparkView>());
		}

		public static ISparkViewEngine StubCreateInstance(this ISparkViewEngine sparkViewEngine, ISparkView view)
		{
			sparkViewEngine.Stub(x => x.CreateInstance(null)).IgnoreArguments().Return(view);
			return sparkViewEngine;
		}
	}
}