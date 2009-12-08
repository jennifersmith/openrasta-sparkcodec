using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Builders
{
	public class ElementTransformerActionsByMatchBuilder : IElementTransformerActionsByMatchBuilder
	{
		private readonly IEnumerable<Tag> _tags;
		private readonly IList<IElementTransformerAction> _actions = new List<IElementTransformerAction>();

		public ElementTransformerActionsByMatchBuilder(IEnumerable<Tag> tags)
		{
			_tags = tags;
		}

		public void AddAction(IElementTransformerAction action)
		{
			_actions.Add(action);
		}
		public ElementTransformerActionsByMatch Build()
		{
			return new ElementTransformerActionsByMatch(_tags, _actions);
		}

	}
}