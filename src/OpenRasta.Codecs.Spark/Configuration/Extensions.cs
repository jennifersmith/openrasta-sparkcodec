using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public static class Extensions
	{
		public static ICodecDefinition AndRenderedBySpark(this ICodecParentDefinition codecParentDefinition,
		                                                  string config)
		{
			return codecParentDefinition.TranscodedBy<SparkCodec>(new {TemplateName = config});
		}

		public static void SparkCodec(this IUses uses)
		{
			DependencyManager.GetService<IDependencyResolver>().AddDependency<ISparkConfiguration, SparkConfiguration>();
			DependencyManager.GetService<IDependencyResolver>().AddDependency<ISparkCodecNamespacesConfiguration, SparkCodecNamespacesConfiguration>();
			DependencyManager.GetService<IDependencyResolver>().AddDependency<ISparkServiceContainerFactory,SparkServiceContainerFactory>();
		}
	}
}