using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Specification.Builders
{
	public class ElementTransformerActionsByMatchBuilder : IElementTransformerActionsByMatchBuilder
	{
		private readonly IEnumerable<Tag> _tags;
		private readonly IList<IElementTransformerAction> _actions = new List<IElementTransformerAction>();
		private readonly IList<IElementTransformerAction> _finalActions = new List<IElementTransformerAction>();

		public ElementTransformerActionsByMatchBuilder(IEnumerable<Tag> tags)
		{
			_tags = tags;
		}

		public IEnumerable<IElementTransformerAction> Actions
		{
			get {
				return _actions;
			}
		}
		public IEnumerable<IElementTransformerAction> FinalActions
		{
			get
			{
				return _finalActions;
			}
		}

		public void AddAction(IElementTransformerAction action)
		{
			_actions.Add(action);
		}
		public ElementTransformerActionsByMatch Build()
		{
			return new ElementTransformerActionsByMatch(_tags, _actions.ToArray(), _finalActions.ToArray());
		}

		public void AddFinalAction(IElementTransformerAction elementTransformerAction)
		{
			_finalActions.Add(elementTransformerAction);
		}
	}
}