using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public static class ElementMatcherExtensions
	{
		public static bool MatchesAtLeastOne(this IEnumerable<Tag> nodeMatchers, Tag tag)
		{
			return nodeMatchers.Contains(x => x == tag);
		}
	}

	public enum ElementMatchResult
	{
		NoMatch,
		Match
	}

}
