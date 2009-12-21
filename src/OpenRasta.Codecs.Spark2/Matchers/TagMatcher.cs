using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public static class TagMatcherExtensions
	{
		public static bool MatchesAtLeastOne(this IEnumerable<Tag> nodeMatchers, Tag tag)
		{
			return nodeMatchers.Contains(x => x.Matches(tag));
		}
	}

	public enum ElementMatchResult
	{
		NoMatch,
		Match
	}

}
