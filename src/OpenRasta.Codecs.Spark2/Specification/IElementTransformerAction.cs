using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public interface IElementTransformerAction
	{
		void Do(IElement Element);
	}
}