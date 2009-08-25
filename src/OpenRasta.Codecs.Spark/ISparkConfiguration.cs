using Spark;

namespace OpenRasta.Codecs.Spark
{
	public interface ISparkConfiguration
	{
		ISparkSettings CreateSettings();
	}
}