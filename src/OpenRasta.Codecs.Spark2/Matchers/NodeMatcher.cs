using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public static class ElementMatcherExtensions
	{
		public static ElementMatchResult MatchesAtLeastOne(this IEnumerable<NodeMatcher> nodeMatchers, IElement element)
		{
			return nodeMatchers.Select(x => x.Match(element)).Where(x => x == ElementMatchResult.Match).FirstOrDefault();
		}
	}
	public class NodeMatcher
	{
		private readonly string _elementName;

		public NodeMatcher(string elementName)
		{
			_elementName = elementName;
		}

		public ElementMatchResult Match(IElement element)
		{
			return NodeNameMatches(element) ? ElementMatchResult.Match : ElementMatchResult.NoMatch;
		}

		private bool NodeNameMatches(IElement element)
		{
			return string.Equals(element.Name, _elementName, StringComparison.InvariantCultureIgnoreCase);
		}
	}

	public enum ElementMatchResult
	{
		NoMatch = 0,
		Match = 1
	}

}
