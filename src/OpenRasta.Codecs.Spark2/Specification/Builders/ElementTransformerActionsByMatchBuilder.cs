using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Matchers;

namespace OpenRasta.Codecs.Spark2.Specification.Builders
{
	public class ElementTransformerActionsByMatchBuilder : IElementTransformerActionsByMatchBuilder
	{
		private readonly IEnumerable<NodeMatcher> _matchers;
		private readonly IList<IElementTransformerAction> _actions = new List<IElementTransformerAction>();

		public ElementTransformerActionsByMatchBuilder(IEnumerable<NodeMatcher> matchers)
		{
			_matchers = matchers;
		}

		public void AddAction(IElementTransformerAction action)
		{
			_actions.Add(action);
		}
		public ElementTransformerActionsByMatch Build()
		{
			return new ElementTransformerActionsByMatch(_matchers, _actions);
		}

	}
}