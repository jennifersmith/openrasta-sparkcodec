using OpenRasta.Codecs.Spark2.Specification.Actions;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public static class Convert
	{
		public static IElementTransformerAction ToAttributeToHref()
		{
			return new ConvertAttributeToUriAction("to", "href", null);
		}
		public static IElementTransformerAction ToTypeAttributeToHref()
		{
			return new ConvertAttributeToUriAction("totype", "href", null);
		}

	}
}