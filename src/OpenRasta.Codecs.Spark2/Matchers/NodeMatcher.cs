using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public static class NodeMatcherExtensions
	{
		public static NodeMatchResult MatchesAtLeastOne(this IEnumerable<NodeMatcher> nodeMatchers, INode node)
		{
			return nodeMatchers.Select(x => x.Match(node)).Where(x => x == NodeMatchResult.Match).FirstOrDefault();
		}
	}
	public class NodeMatcher
	{
		private readonly string _elementName;

		public NodeMatcher(string elementName)
		{
			_elementName = elementName;
		}

		public NodeMatchResult Match(INode element)
		{
			return NodeNameMatches(element) ? NodeMatchResult.Match : NodeMatchResult.NoMatch;
		}

		private bool NodeNameMatches(INode element)
		{
			return string.Equals(element.Name, _elementName, StringComparison.InvariantCultureIgnoreCase);
		}
	}

	public enum NodeMatchResult
	{
		NoMatch = 0,
		Match = 1
	}

}
