using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark2.Specification.Actions
{
	public interface IAttributeModifer
	{
		void Modify(IAttribute originalAttribute, IAttribute newAttribute);
	}
	public class ConvertAttributeAction : IElementTransformerAction
	{
		private readonly string _toAttribute;
		private readonly string _fromAttribute;
		private readonly IAttributeModifer _attributeModifer;

		public ConvertAttributeAction(string toAttribute, string fromAttribute, IAttributeModifer attributeModifer)
		{
			_toAttribute = toAttribute;
			_fromAttribute = fromAttribute;
			_attributeModifer = attributeModifer;
		}

		public void Do(IElement element)
		{
			if (element.HasAttribute(_fromAttribute) == false)
			{
				return;
			}
			IAttribute fromAttribute = element.GetAttribute(_fromAttribute);
			IAttribute attribute = element.AddAttribute(_toAttribute);
			_attributeModifer.Modify(fromAttribute, attribute);
			element.RemoveAttribute(fromAttribute);
		}
	}
	public class CreateUriActionModifier : IAttributeModifer
	{
		private readonly ISyntaxProvider _syntaxProvider;

		public CreateUriActionModifier(ISyntaxProvider syntaxProvider)
		{
			_syntaxProvider = syntaxProvider;
		}

		private ConditionalExpression GetConditionalResourceExpression(IAttribute fromAttribute)
		{
			string targetResource = fromAttribute.GetTextValue();
			string nullCheck = _syntaxProvider.CreateNullCheckExpression(targetResource);
			string uriExpression = _syntaxProvider.CreateUriExpression(targetResource);
			return new ConditionalExpression(nullCheck, uriExpression);
		}

		public void Modify(IAttribute originalAttribute, IAttribute newAttribute)
		{
			IConditionalExpressionNodeWrapper expression = newAttribute.AddConditionalExpressionNode();
			ConditionalExpression conditionalExpression = GetConditionalResourceExpression(originalAttribute);
			expression.SetExpressionBody(conditionalExpression);
		}
	}
}