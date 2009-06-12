using OpenRasta.Configuration.Fluent;
using OpenRasta.Configuration.Fluent.Implementation;
using OpenRasta.DI;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public static class Extensions
	{
		public static ICodecDefinition AndRenderedBySpark(this ICodecParentDefinition codecParentDefinition,
			                              string config)
		{
            return codecParentDefinition.TranscodedBy<SparkCodec>(new{TemplateName=config});
		}

		public static void SparkCodec(this IUses uses)
		{
			DependencyManager.GetService<IDependencyResolver>().AddDependency(typeof (ISparkConfiguration),
			                                                                  typeof (SparkConfiguration),
			                                                                  DependencyLifetime.Singleton);
		}
	}
}