using System;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class ConvertAttributeToUriAction : IElementTransformerAction
	{
		private readonly string _toAttribute;
		private readonly string _fromAttribute;
		private readonly ISyntaxProvider _syntaxProvider;

		public ConvertAttributeToUriAction(string toAttribute, string fromAttribute, ISyntaxProvider syntaxProvider)
		{
			_toAttribute = toAttribute;
			_fromAttribute = fromAttribute;
			_syntaxProvider = syntaxProvider;
		}

		public void Do(IElement element)
		{
			if(element.HasAttribute(_fromAttribute)==false)
			{
				return;
			}
			IAttribute fromAttribute = element.GetAttribute(_fromAttribute);
			IAttribute attribute = element.AddAttribute(_toAttribute);
			ICodeExpressionNode expression = attribute.AddExpression();
			expression.SetExpressionBody(_syntaxProvider.CreateUriExpression(fromAttribute.GetTextValue())); 
		}
	}
}