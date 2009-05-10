using Spark;

namespace OpenRasta.Codecs.Spark
{
	public interface ISparkConfiguration	
	{
		ISparkServiceContainer Container { get; }
	}
}