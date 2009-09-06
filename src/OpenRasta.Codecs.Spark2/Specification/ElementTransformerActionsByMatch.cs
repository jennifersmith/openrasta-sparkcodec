using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Specification;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public struct ElementTransformerActionsByMatch
	{
		private readonly IEnumerable<NodeMatcher> _nodeMatchers;
		private readonly IEnumerable<IElementTransformerAction> _elementTransformerActions;

		public ElementTransformerActionsByMatch(IEnumerable<NodeMatcher> nodeMatchers, IEnumerable<IElementTransformerAction> elementTransformerActions)
		{
			_nodeMatchers = nodeMatchers;
			_elementTransformerActions = elementTransformerActions;
		}

		public IEnumerable<IElementTransformerAction> ElementTransformerActions
		{
			get { return _elementTransformerActions; }
		}

		public IEnumerable<NodeMatcher> NodeMatchers
		{
			get { return _nodeMatchers; }
		}
	}
}