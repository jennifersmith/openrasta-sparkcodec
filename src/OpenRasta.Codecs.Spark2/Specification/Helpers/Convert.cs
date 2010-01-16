using System;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Helpers
{
	public static class Remove
	{
		public static IElementTransformerAction ToAttributes() { return new RemoveAttributeAction("To"); }
		public static IElementTransformerAction ToTypeAttributes() { return new RemoveAttributeAction("ToType"); }
		public static IElementTransformerAction ForAttributes() { return new RemoveAttributeAction("For"); }
		public static IElementTransformerAction ForTypeAttributes() { return new RemoveAttributeAction("ForType"); }
	}
	public static class Convert
	{
		public static IElementTransformerAction ToAttributeToHref()
		{
			return CreateToAttributeToCreateUriConverter("href", "to");
		}

		public static IElementTransformerAction ToTypeAttributeToHref()
		{
			return CreateToAttributeToCreateUriFromTypeConverter("href", "totype");
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

		public static IElementTransformerAction ForTypeAttributeToAction()
		{
			return CreateToAttributeToCreateUriFromTypeConverter("action", "fortype");
		}

		public static IElementTransformerAction ToTypeAttributeToSrc()
		{
			return CreateToAttributeToCreateUriFromTypeConverter("src", "totype");
		}

		public static IElementTransformerAction ForAttributeToName()
		{
			return CreateToAttributeToPropertyPathConverter("name", "for");
		}

		private static IElementTransformerAction CreateToAttributeToPropertyPathConverter(string toAttribute, string fromAttribute)
		{
			return new ConvertAttributeAction(toAttribute, fromAttribute, new PropertyPathActionModifier(new CSharpSyntaxProvider()));
		}

		public static IElementTransformerAction ResourceValueToInnerText()
		{
			return new ConvertPropertyValueToInnerText(new CSharpSyntaxProvider());
		}

		public static IElementTransformerAction ResourceValueToCheckedAttribute()
		{
			return new ConvertAttributeAction("checked", "for", new ValueToConditionalAttribute(new CSharpSyntaxProvider()));
		}

		public static IElementTransformerAction ResourceValueToSelectedOption()
		{
			return new ConvertResourceValueToSelectedOptionAction(new CSharpSyntaxProvider());
		}
	}

}