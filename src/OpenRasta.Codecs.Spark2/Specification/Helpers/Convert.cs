using OpenRasta.Codecs.Spark2.Specification.Actions;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public static class Convert
	{
		public static IElementTransformerAction ToAttributeToHref()
		{
			return CreateToAttributeConverter("href");
		}

		public static IElementTransformerAction ToTypeAttributeToHref()
		{
			return new ConvertAttributeToUriAction("href", "totype", new CSharpSyntaxProvider());
		}

		public static IElementTransformerAction ToAttributeToSrc()
		{
			return CreateToAttributeConverter("src");
		}

		private static IElementTransformerAction CreateToAttributeConverter(string toAttribute)
		{
			return new ConvertAttributeToUriAction(toAttribute, "to", new CSharpSyntaxProvider());
		}

		public static IElementTransformerAction ForAttributeToAction()
		{
			return new ConvertAttributeToUriAction("action", "for", new CSharpSyntaxProvider());
		}
	}
}