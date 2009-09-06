using System;
using System.Collections.Generic;
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

		public static AttributeNode BasicAttributeNode()
		{
			return new AttributeNode("attributeName", "attributeValue");
		}
		public static Node UnknownNode()
		{
			return new UnknownNodeImpl();
		}
		private class UnknownNodeImpl : Node
		{

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
			return new SparkAttributeWrapper(SparkTestNodes.BasicAttributeNode());
		}
		public static TestElement TestElement(string elementName)
		{
			return new TestElement(elementName);
		}
	}

	public class TestElement : IElement
	{
		private readonly string _elementName;

		public TestElement(string elementName)
		{
			_elementName = elementName;
		}

		public string Name
		{
			get { return _elementName; }
		}
	}
}