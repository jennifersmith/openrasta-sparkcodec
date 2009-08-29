using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Transformers
{
	public interface IElementTransformer
	{
		IElement Transform(IEnumerable<INode> body);
	}
}