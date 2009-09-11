using OpenRasta.Codecs.Spark2.Specification.Actions;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public static class Convert
	{
		public static IElementTransformerAction ToAttributeToHref()
		{
			return new ConvertAttributeToUriAction("href", "to", new CSharpSyntaxProvider());
		}
		public static IElementTransformerAction ToTypeAttributeToHref()
		{
			return new ConvertAttributeToUriAction("href", "totype", new CSharpSyntaxProvider());
		}

	}
}