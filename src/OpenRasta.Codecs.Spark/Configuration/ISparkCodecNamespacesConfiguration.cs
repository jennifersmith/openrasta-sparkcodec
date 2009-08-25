using Spark;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public interface ISparkCodecNamespacesConfiguration
	{
		void AddNamespaces(SparkSettings settings);
	}
}