using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public interface IElementTransformerSpecification
	{
		IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element);
		IEnumerable<IElementTransformerAction> GetActionsForTag(Tag tag);
	}
}