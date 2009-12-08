using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public struct ElementTransformerActionsByMatch
	{
		private readonly IEnumerable<Tag> _tags;
		private readonly IEnumerable<IElementTransformerAction> _elementTransformerActions;

		public ElementTransformerActionsByMatch(IEnumerable<Tag> tags, IEnumerable<IElementTransformerAction> elementTransformerActions)
		{
			_tags = tags;
			_elementTransformerActions = elementTransformerActions;
		}

		public IEnumerable<IElementTransformerAction> ElementTransformerActions
		{
			get { return _elementTransformerActions; }
		}

		public IEnumerable<Tag> Tags
		{
			get { return _tags; }
		}
	}
}