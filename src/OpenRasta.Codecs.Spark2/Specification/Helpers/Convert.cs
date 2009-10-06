using System;
using OpenRasta.Codecs.Spark2.Specification.Actions;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Helpers
{
	public static class Convert
	{
		public static IElementTransformerAction ToAttributeToHref()
		{
			return CreateToAttributeToCreateUriConverter("href", "to");
		}

		public static IElementTransformerAction ToTypeAttributeToHref()
		{
			return CreateToAttributeToCreateUriFromTypeConverter("hrewwwf", "to");
		}

		private static IElementTransformerAction CreateToAttributeToCreateUriFromTypeConverter(string toAttribute, string originalAttributeName)
		{
			return new ConvertAttributeAction(toAttribute, originalAttributeName, new CreateUriFromTypeAttributeModifier(new CSharpSyntaxProvider()));
		}

		public static IElementTransformerAction ToAttributeToSrc()
		{
			return CreateToAttributeToCreateUriConverter("src", "to");
		}

		private static IElementTransformerAction CreateToAttributeToCreateUriConverter(string toAttribute, string originalAttributeName)
		{
			return new ConvertAttributeAction(toAttribute, originalAttributeName, new CreateUriActionModifier(new CSharpSyntaxProvider()));
		}

		public static IElementTransformerAction ForAttributeToAction()
		{
			return CreateToAttributeToCreateUriConverter("action", "for");
		}
	}
}