using Spark;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public class SparkCodecNamespacesConfiguration : ISparkCodecNamespacesConfiguration
	{
		static readonly  string[] Namespaces = new[] {"System.Collections", "System.Collections.Generic", "OpenRasta.Web.Markup", "OpenRasta.Web", "OpenRasta.Codecs.Spark", "System.Linq", "OpenRasta.Codecs.Spark.Configuration.Syntax" };

		public void AddNamespaces(SparkSettings settings)
		{
			Namespaces.ForEach(ns => settings.AddNamespace(ns));
		}
	}
}