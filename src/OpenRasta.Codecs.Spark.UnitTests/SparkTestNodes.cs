using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public static class SparkTestNodes
	{
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
	}

	public static class InternalTestNodes
	{
		public static IElement BasicElementNode()
		{
			return new SparkElementWrapper(SparkTestNodes.BasicElementNode());
		}

		public static INode BasicAttributeNode()
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


		protected IList<INode> Nodes
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
	}
	public class TestCodeExpressionNode : ICodeExpressionNode
	{
		private string _body;

		public string Body
		{
			get {
				return _body;
			}
		}

		public void SetExpressionBody(string expression)
		{
			_body = expression;
		}
	}
}