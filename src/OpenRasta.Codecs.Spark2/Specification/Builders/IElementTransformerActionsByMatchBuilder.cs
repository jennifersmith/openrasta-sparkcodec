using OpenRasta.Codecs.Spark2.Matchers;

namespace OpenRasta.Codecs.Spark2.Specification.Builders
{
	public interface IElementTransformerActionsByMatchBuilder
	{
		void AddAction(IElementTransformerAction ElementTransformerAction);
		ElementTransformerActionsByMatch Build();
	}
}