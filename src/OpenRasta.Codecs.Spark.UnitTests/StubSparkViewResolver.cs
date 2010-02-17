using Rhino.Mocks;
using Spark;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	internal class StubSparkViewResolver : ISparkViewResolver
	{
		public ISparkView Create(string name, object viewData)
		{
			return MockRepository.GenerateStub<ISparkView>();
		}
	}
}