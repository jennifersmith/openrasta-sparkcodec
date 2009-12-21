using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public class PropertyPathActionModifier : IAttributeModifer
	{
		private readonly ISyntaxProvider _syntaxProvider;

		public PropertyPathActionModifier(ISyntaxProvider syntaxProvider)
		{
			_syntaxProvider = syntaxProvider;
		}

		public void Modify(IAttribute originalAttribute, IAttribute newAttribute)
		{
			ICodeExpressionNode node = newAttribute.AddCodeExpressionNode();
			string expression = _syntaxProvider.CreateGetPropertyPathExpression(originalAttribute.GetTextValue());
			node.SetExpressionBody(
				new CodeExpression(expression)
				);
		}
	}
}