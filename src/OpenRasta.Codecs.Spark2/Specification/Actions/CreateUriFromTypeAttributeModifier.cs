using System;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class CreateUriFromTypeAttributeModifier : IAttributeModifer
	{
		private readonly ISyntaxProvider _syntaxProvider;

		public CreateUriFromTypeAttributeModifier( ISyntaxProvider syntaxProvider)
		{
			_syntaxProvider = syntaxProvider;
		}


		private CodeExpression GetCreateUriFromTypeExpression(IAttribute fromAttribute)
		{
			string uriExpression = _syntaxProvider.CreateUriFromTypeExpression(fromAttribute.GetTextValue());
			return new CodeExpression(uriExpression);
		}

		public void Modify(IAttribute originalAttribute, IAttribute newAttribute)
		{
			var expression = newAttribute.AddCodeExpressionNode();
			CodeExpression conditionalExpression = GetCreateUriFromTypeExpression(originalAttribute);
			expression.SetExpressionBody(conditionalExpression);
		}
	}
}