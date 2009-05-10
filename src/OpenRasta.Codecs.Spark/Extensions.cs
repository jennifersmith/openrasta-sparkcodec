using System.Linq;
using OpenRasta.Configuration.Fluent;

namespace OpenRasta.Codecs.Spark
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

	}
}
