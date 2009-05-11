using System.Linq;
using OpenRasta.Configuration.Fluent;
using OpenRasta.DI;

namespace OpenRasta.Codecs.Spark.Configuration
{
	public static class Extensions
	{
		/// <summary>
		/// Adds an html rendering of a resource using an aspx page.
		/// </summary>
		/// <typeparam name="TResource"></typeparam>
		/// <typeparam name="TResourceHandler"></typeparam>
		/// <param name="target"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static ResourcesOfTypeAreTranscodedBy<TResource, SparkCodec> AndRenderedBySpark
			<TResource, TResourceHandler>(this ResourcesOfTypeAreHandledBy<TResource, TResourceHandler> target,
			                              string config)
		{
			return new ResourcesOfTypeAreTranscodedBy<TResource, SparkCodec>(new { index = config });
		}

		public static void SparkCodec(this IUses uses)
		{
			DependencyManager.GetService<IDependencyResolver>().AddDependency(typeof(ISparkConfiguration),
			                                                                  typeof(SparkConfiguration), DependencyLifetime.Singleton);
		}

	}
}