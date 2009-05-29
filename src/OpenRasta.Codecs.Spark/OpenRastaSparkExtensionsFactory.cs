using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark.Extensions;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark
{
	internal class OpenRastaSparkExtensionsFactory : ISparkExtensionFactory
	{
		#region ISparkExtensionFactory Members

		public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
		{
			IEnumerable<IReplacement> replacements = GetApplicableReplacements(node);
			if (replacements.Any())
			{
				return new SparkExtension(node, replacements);
			}
			return null;
		}

		#endregion

		private static IEnumerable<IReplacement> GetApplicableReplacements(ElementNode node)
		{
			var result = new List<IReplacement>();
			result.AddRange(UriReplacementSpecifications.GetMatching(node));
			result.AddRange(FormReplacementSpecifications.GetMatching(node));
			return result;
		}
	}
}