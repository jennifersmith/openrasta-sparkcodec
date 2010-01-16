using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class ConvertResourceValueToSelectedOptionAction : IElementTransformerAction
	{
		private ISyntaxProvider _syntaxProvider;

		public ConvertResourceValueToSelectedOptionAction(ISyntaxProvider syntaxProvider)
		{
			_syntaxProvider = syntaxProvider;
		}

		public void Do(IElement element)
		{
			string resourceValue = element.GetAttribute("for").GetTextValue();
			IEnumerable<IElement> childElements = element.GetChildElements("option");
			childElements.ForEach(x=>
			                      	{
										if(x.HasAttribute("selected"))
										{
											x.RemoveAttribute(x.GetAttribute("selected"));
										}
			                      		IConditionalExpressionNodeWrapper wrapper = x.AddAttribute("selected").AddConditionalExpressionNode();
										wrapper.SetExpressionBody(new ConditionalExpression(_syntaxProvider.CreateNullCheckAndEvalExpression(resourceValue), resourceValue));
			                      	});
		}
	}
}