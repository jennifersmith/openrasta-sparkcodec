using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public struct ElementTransformerActionsByMatch
	{
		private readonly IEnumerable<Tag> _tags;
		private readonly IEnumerable<IElementTransformerAction> _elementTransformerActions;
		private IEnumerable<IElementTransformerAction> _finalActions;

		public ElementTransformerActionsByMatch(IEnumerable<Tag> tags, IEnumerable<IElementTransformerAction> elementTransformerActions, IEnumerable<IElementTransformerAction> finalActions)
		{
			_tags = tags;
			_elementTransformerActions = elementTransformerActions;
			_finalActions = finalActions;
		}

		public IEnumerable<IElementTransformerAction> ElementTransformerActions
		{
			get { return _elementTransformerActions; }
		}

		public IEnumerable<Tag> Tags
		{
			get { return _tags; }
		}

		public IEnumerable<IElementTransformerAction> FinalElementTransformerActions
		{
			get { return _finalActions; }
		}
	}
}