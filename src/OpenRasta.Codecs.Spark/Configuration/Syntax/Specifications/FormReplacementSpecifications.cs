using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark.Extensions;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Configuration.Syntax.Specifications
{
	internal class FormReplacementSpecifications
	{
		public static readonly IEnumerable<ReplacementSpecification> AllSpecifications =
			GetInputTagReplacementSpecifications();

		public static readonly IEnumerable<ReplacementSpecification> SelectSpecs = GetSelectSpects();


		public static readonly IEnumerable<ReplacementSpecification> TextAreaSpecs = GetTextAreaSpecs();

		private static IEnumerable<ReplacementSpecification> GetInputTagReplacementSpecifications()
		{
			return new List<ReplacementSpecification>
			       	{
			       		// all attribute/elements needing uri replacement
	
			       		new ReplacementSpecification("input", "for"),
			       		new ReplacementSpecification("textarea", "for"),
			       		new ReplacementSpecification("select", "for"),
			       	};
		}

		private static IEnumerable<ReplacementSpecification> GetTextAreaSpecs()
		{
			return new List<ReplacementSpecification>
			       	{
			       		new ReplacementSpecification("textarea", "for")
			       	};
		}

		private static IEnumerable<ReplacementSpecification> GetSelectSpects()
		{
			return new List<ReplacementSpecification>
			       	{
			       		new ReplacementSpecification("select", "for")
			       	};
		}

		public static IEnumerable<IReplacement> GetMatching(ElementNode node)
		{
			// should create the urireplacements on static construction
			var result = new List<IReplacement>();
			result.AddRange(GetNameReplacements(node));
			result.AddRange(GetValueReplacements(node));
			result.AddRange(GetMiscReplacements(node));
			result.AddRange(GetTidyReplacements(node));
			// todo: refactor this shit
			return result;
		}

	
		private static IEnumerable<IReplacement> GetTidyReplacements(ElementNode node)
		{
			return MatchingSpecs(node).Select(x => new TidyUpReplacement(x)).Cast<IReplacement>();
		}

		private static IEnumerable<IReplacement> GetMiscReplacements(ElementNode node)
		{
			IEnumerable<IReplacement> result =
				TextAreaSpecs.Where(x => x.IsMatch(node)).Select(x => new TextAreaValueReplacement(x)).Cast<IReplacement>();
			result =
				result.Union(
					SelectSpecs.Where(x => x.IsMatch(node)).Select(x => new SelectSelectedValueReplacement(x)).Cast<IReplacement>());
			
			return result;
		}

		private static IEnumerable<IReplacement> GetNameReplacements(ElementNode node)
		{
			return MatchingSpecs(node).Select(x => new InputNameReplacement(x)).Cast<IReplacement>();
		}

		private static IEnumerable<IReplacement> GetValueReplacements(ElementNode node)
		{
			return MatchingSpecs(node).Select(x => new InputValueReplacement(x)).Cast<IReplacement>();
		}

		private static IEnumerable<ReplacementSpecification> MatchingSpecs(ElementNode node)
		{
			return AllSpecifications.Where(x => x.IsMatch(node));
		}
	}
}