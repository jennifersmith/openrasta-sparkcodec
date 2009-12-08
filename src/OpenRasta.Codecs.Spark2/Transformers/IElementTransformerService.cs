using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Transformers
{
	public interface IElementTransformerService
	{
		IElementTransformer GetTransformerFor(IElement element);
		bool IsTransformable(IElement element);
		bool IsTransformable(Tag tag);
		IElementTransformer GetTransformerFor(Tag tag);
	}
}