using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public interface IElementTransformerSpecification
	{
		IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element);
		IEnumerable<IElementTransformerAction> GetActionsForTag(Tag tag);
	}
}