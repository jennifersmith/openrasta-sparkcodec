using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public class ElementTransformerSpecification : IElementTransformerSpecification
	{
		private readonly IEnumerable<ElementTransformerActionsByMatch> _elementTransformerActionsByMatchs;

		public ElementTransformerSpecification(IEnumerable<ElementTransformerActionsByMatch> elementTransformerActionsByMatchs)
		{
			_elementTransformerActionsByMatchs = elementTransformerActionsByMatchs;
		}

		public IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element)
		{
			var allMatches =
				_elementTransformerActionsByMatchs.Where(x => x.NodeMatchers.MatchesAtLeastOne(element) == NodeMatchResult.Match);

			return allMatches.SelectMany(x => x.ElementTransformerActions);
		}
	}
}