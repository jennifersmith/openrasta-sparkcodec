using System.Collections.Generic;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	//internal class FormExtension : SparkExtensionBase
	//{
	//    public FormExtension(ElementNode node) : base(node)
	//    {
	//    }

	//    protected override void Transform(ElementNode elementNode, IList<Node> body)
	//    {
	//        AttributeNode forAttribute = Node.GetAttribute("for");
	//        AttributeNode forType = Node.GetAttribute("fortype");
	//        ExpressionNode actionExpression;
	//        if (forAttribute != null)
	//        {
	//            elementNode.Attributes.Remove(forAttribute);
	//            actionExpression = new ExpressionNode(forAttribute.Value.GetCreateUriSnippet(false));
	//        }
	//        else
	//        {
	//            elementNode.Attributes.Remove(forType);
	//            actionExpression = new ExpressionNode(forType.Value.GetCreateUriSnippet(true));
	//        }
	//        elementNode.Attributes.Add(new AttributeNode("action", new List<Node> {actionExpression}));
	//    }
	//}
}