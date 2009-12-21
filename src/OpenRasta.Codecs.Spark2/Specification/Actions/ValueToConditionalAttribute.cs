using System;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class ValueToConditionalAttribute : IAttributeModifer
	{
		private ISyntaxProvider _syntaxProvider;

		public ValueToConditionalAttribute(ISyntaxProvider syntaxProvider)
		{
			_syntaxProvider = syntaxProvider;
		}

		public void Modify(IAttribute originalAttribute, IAttribute newAttribute)
		{
			ICodeExpressionNode codeExpressionNode = newAttribute.AddCodeExpressionNode();
			codeExpressionNode.SetExpressionBody(new CodeExpression("true"));
			IConditionalExpressionNodeWrapper conditionalExpressionNodeWrapper = newAttribute.AddConditionalExpressionNode();
			conditionalExpressionNodeWrapper.SetExpressionBody(new ConditionalExpression(_syntaxProvider.CreateNullCheckAndEvalExpression(originalAttribute.GetTextValue()), originalAttribute.GetTextValue()));

		}
	}
}