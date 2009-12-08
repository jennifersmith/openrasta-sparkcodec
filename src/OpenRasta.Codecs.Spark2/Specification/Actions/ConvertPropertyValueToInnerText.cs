using System;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{	
	public class ConvertPropertyValueToInnerText : IElementTransformerAction
	{
		private ISyntaxProvider _syntaxProvider;

		public ConvertPropertyValueToInnerText(ISyntaxProvider syntaxProvider)
		{
			_syntaxProvider = syntaxProvider;
		}

		public void Do(IElement element)
		{
			if(element.HasAttribute("for"))
			{
				IAttribute attribute = element.GetAttribute("for");
				IConditionalExpressionNodeWrapper codeExpressionNode = element.AddConditionalExpressionNode();
				string conditional = _syntaxProvider.CreateNullCheckExpression(attribute.GetTextValue().Split('.').First());
				codeExpressionNode.SetExpressionBody(new ConditionalExpression(conditional, attribute.GetTextValue()));
				element.ClearInnerText();
			}
		}
	}
}