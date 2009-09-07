using OpenRasta.Codecs.Spark2.Specification.Builders;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public static class ElementTransformerExtensions
	{
		public static IElementTransformerActionsByMatchBuilder Do(this IElementTransformerActionsByMatchBuilder elementTransformerActionsByMatchBuilder, params IElementTransformerAction[] actions)
		{
			actions.ForEach(elementTransformerActionsByMatchBuilder.AddAction);
			return elementTransformerActionsByMatchBuilder;
		}
	}
}