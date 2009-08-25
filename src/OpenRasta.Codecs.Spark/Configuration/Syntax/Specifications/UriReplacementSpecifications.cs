using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Configuration.Syntax.Specifications
{
	internal static class UriReplacementSpecifications
	{
		public static readonly IEnumerable<ReplacementSpecification> AllSpecifications = GetUriReplacementSpecifications();

		private static IEnumerable<ReplacementSpecification> GetUriReplacementSpecifications()
		{
			return new List<ReplacementSpecification>
			       	{
			       		// all attribute/elements needing uri replacement
	
			       		new ReplacementSpecification("iframe", "to", "src"),
			       		new ReplacementSpecification("a", "to", "href"),
			       		new ReplacementSpecification("img", "to", "src"),
			       		new ReplacementSpecification("form", "for", "action"),
			       		new ReplacementSpecification("iframe", "totype", "src"),
			       		new ReplacementSpecification("a", "totype", "href"),
			       		new ReplacementSpecification("img", "totype", "src"),
			       		new ReplacementSpecification("form", "fortype", "action")
			       	};
		}

		public static IEnumerable<IReplacement> GetMatching(ElementNode node)
		{
			// should create the urireplacements on static construction
			return AllSpecifications.Where(x => x.IsMatch(node)).Select(x => new UriReplacement(x)).Cast<IReplacement>();
		}
	}
}