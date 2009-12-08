using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public class ElementTransformerSpecification : IElementTransformerSpecification
	{
		private readonly IEnumerable<ElementTransformerActionsByMatch> _elementTransformerActionsByMatchs;

		public ElementTransformerSpecification(IEnumerable<ElementTransformerActionsByMatch> elementTransformerActionsByMatchs)
		{
			_elementTransformerActionsByMatchs = elementTransformerActionsByMatchs;
		}

		public IEnumerable<IElementTransformerAction> GetActionsForTag(Tag tag)
		{
			var allMatches =
				_elementTransformerActionsByMatchs.Where(x => x.Tags.MatchesAtLeastOne(tag));

			return allMatches.SelectMany(x => x.ElementTransformerActions);
		}
		public IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element)
		{
			throw new Exception("Use the other one");
		}

		public IEnumerable<ElementTransformerActionsByMatch> GetElementTransformerActionsByMatch()
		{
			return _elementTransformerActionsByMatchs.ToArray();
		}
	}
}