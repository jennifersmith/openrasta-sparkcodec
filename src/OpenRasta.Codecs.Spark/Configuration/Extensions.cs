using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;
using Spark;

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
			IDependencyResolver resolver = DependencyManager.GetService<IDependencyResolver>();
			resolver.AddDependency<ISparkConfiguration, SparkConfiguration>();
			resolver.AddDependency<ISparkCodecNamespacesConfiguration, SparkCodecNamespacesConfiguration>();

			// new stuff
			resolver.AddDependency<ISparkServiceContainerFactory, SparkServiceContainerFactory>();
			resolver.AddDependency<ISparkRenderer, SparkRenderer>();
			resolver.AddDependency<ISparkViewResolver, SparkViewResolverWithServiceContainerWrapper>();
			resolver.AddDependency<ISparkServiceContainerFactory, SparkServiceContainerFactory>();
		}
	}
}