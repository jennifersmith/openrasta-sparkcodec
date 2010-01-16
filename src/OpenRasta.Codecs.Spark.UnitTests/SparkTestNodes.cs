using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public static class SparkTestNodes
	{
		public static ElementNode ElementNode(string name)
		{
			return new ElementNode(name, new List<AttributeNode>(), false);
		}
		public static ElementNode BasicElementNode()
		{
			return new ElementNode("elementNode", new List<AttributeNode>(), false);
		}
		public static ElementNode WithAttribute(this ElementNode elementNode, string name, string value)
		{
			elementNode.Attributes.Add(new AttributeNode(name,value));
			return elementNode;
		}
		public static AttributeNode BasicAttributeNode(string attributeName)
		{
			return new AttributeNode(attributeName, "attributeValue");
		}
		public static AttributeNode WithText(this AttributeNode attributeNode, string text)
		{
			attributeNode.Nodes.Clear();
			attributeNode.Nodes.Add(new TextNode(text));
			return attributeNode;
		}
		public static Node UnknownNode()
		{
			return new UnknownNodeImpl();
		}
		private class UnknownNodeImpl : Node
		{

		}

		public static ElementNode WithAttribute(this ElementNode elementnode, AttributeNode attributeNode)
		{
			elementnode.Attributes.Add(attributeNode);
			return elementnode;
		}

		public static ExpressionNode ExpressionNode()
		{
			return new ExpressionNode("");
		}
		public static ConditionNode ConditionNode()
		{
			return new ConditionNode();
		}
	}

	public static class InternalTestNodes
	{
		public static IElement BasicElementNode()
		{
			return new SparkElementWrapper(SparkTestNodes.BasicElementNode(), new Node[0]);
		}

		public static IAttribute BasicAttributeNode()
		{
			return new SparkAttributeWrapper(SparkTestNodes.BasicAttributeNode("attributeName"));
		}
		public static TestElement TestElement(string elementName)
		{
			return new TestElement(elementName);
		}
		public static TestElement WithAttribute(this TestElement testElement, string name, string value)
		{
			testElement.AddAttribute(new TestAttributeNode(name,value));
			return testElement;
		}
	}

	public class ContentContainingNode
	{
		private string _name;
		private readonly IList<INode> _nodes = new List<INode>();

		public string Name
		{
			get { return _name; }
		}
		public ContentContainingNode(string name)
		{
			this._name = name;
		}


		public IList<INode> Nodes
		{
			get { return _nodes; }
		}

		protected void AddNode(INode node)
		{
			Nodes.Add(node);
		}
	}

	public class TestElement : ContentContainingNode, IElement
	{
		public TestElement(string name) : base(name)
		{
		}

		public IEnumerable<IAttribute> Attributes
		{
			get
			{
				return Nodes.Where(x => (x is IAttribute)).Cast<IAttribute>();
			}
		}
		public IEnumerable<IElement> Elements
		{
			get
			{
				return Nodes.Where(x => (x is IElement)).Cast<IElement>();
			}
		}

		public IEnumerable<TestTextNode> TextNodes
		{
			get
			{
				return Nodes.Where(x => (x is TestTextNode)).Cast<TestTextNode>();
			}
		}
		public IEnumerable<IConditionalExpressionNodeWrapper> ConditionalExpressionNodes
		{
			get
			{
				return Nodes.Where(x => (x is IConditionalExpressionNodeWrapper)).Cast<IConditionalExpressionNodeWrapper>();
			}
		}
		public void AddAttribute(IAttribute attribute)
		{
			AddNode(attribute);
		}

		public IAttribute AddAttribute(string attributeName)
		{
			var node = new TestAttributeNode(attributeName, "");
			AddNode(node);
			return node;
		}

		public bool HasAttribute(string attribute)
		{
			return Attributes.Where(x => x.Name.Equals(attribute, StringComparison.InvariantCultureIgnoreCase)).Any();
		}

		public IAttribute GetAttribute(string attribute)
		{
			return Attributes.Where(x => x.Name.Equals(attribute, StringComparison.InvariantCultureIgnoreCase)).First();
		}

		public void RemoveAttribute(IAttribute attribute)
		{
			var toRemove = Attributes.Where(x => x.Name == attribute.Name).FirstOrDefault();
			if(toRemove!=null)
			{
				Nodes.Remove(attribute);				
			}
		}

		public ICodeExpressionNode AddCodeExpressionNode()
		{
			TestCodeExpressionNode node = new TestCodeExpressionNode();
			AddNode(node);
			return node;
		}

		public IConditionalExpressionNodeWrapper AddConditionalExpressionNode()
		{
			TestConditionalExpressionNode node = new TestConditionalExpressionNode();
			Nodes.Add(node);
			return node;
		}

		public void ClearInnerText()
		{
			foreach (var node in TextNodes.ToArray())
			{
				Nodes.Remove(node);
			}
		}

		public IEnumerable<IElement> GetChildElements(string name)
		{
			return Elements.Where(e => e.Name == name);
		}

		public void AddTextElement(string value)
		{
			AddNode(new TestTextNode(value));
		}

		public string InnerText()
		{
			StringBuilder builder = new StringBuilder();
			foreach (var node in TextNodes)
			{
				builder.Append(node.Value);
			}
			return builder.ToString();
		}

		public TestElement WithChildElement(string elementName)
		{
			TestElement	newTestElement = new TestElement(elementName);
			Nodes.Add(newTestElement);
			return this;
		}
		public TestElement WithChildElement(TestElement element)
		{
			Nodes.Add(element);
			return this;
		}
	}

	public class TestTextNode : INode
	{
		private readonly string _value;

		public TestTextNode(string value)
		{
			_value = value;
		}

		public string Value
		{
			get { return _value; }
		}
	}

	public class TestCodeExpressionNode :  ICodeExpressionNode
	{
		private CodeExpression _codeExpression;

		public CodeExpression CodeExpression
		{
			get
			{
				return _codeExpression;
			}
		}

		public void SetExpressionBody(CodeExpression codeExpression)
		{
			_codeExpression = codeExpression;
		}
	}

	public class TestConditionalExpressionNode : IConditionalExpressionNodeWrapper
	{
		private ConditionalExpression _conditionalExpression;
		public ConditionalExpression ConditionalExpression
		{
			get {
				return _conditionalExpression;
			}
		}

		public void SetExpressionBody(ConditionalExpression conditionalExpression)
		{
			_conditionalExpression = conditionalExpression;
		}

		public void SetExpressionBody(CodeExpression codeExpression)
		{
		}
	}
}