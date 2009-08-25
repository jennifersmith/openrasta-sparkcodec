using Spark;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public interface ISparkServiceContainerFactory
	{
		ISparkServiceContainer CreateServiceContainer();
	}
}