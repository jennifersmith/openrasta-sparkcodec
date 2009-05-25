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
			if(replacements.Any())
			{
				return new SparkExtension(node, replacements);	
			}
			return null;
		}

		private IEnumerable<IReplacement> GetApplicableReplacements(ElementNode node)
		{
			List<IReplacement> result = new List<IReplacement>();
			result.AddRange(UriReplacementSpecifications.GetMatching(node));
			return result;
		}

		#endregion

		private static bool IsResourceAnchorTag(ElementNode node)
		{
			return node.IsTag("a") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}

		private static bool IsResourceFormTag(ElementNode node)
		{
			return node.IsTag("form") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}

		private static bool IsInputTag(ElementNode node)
		{
			return node.IsTag("input") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}

		private static bool IsTextareaTag(ElementNode node)
		{
			return node.IsTag("textarea") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}
	}
}