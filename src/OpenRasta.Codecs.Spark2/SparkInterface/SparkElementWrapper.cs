using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkElementWrapper : SparkNodeWrapper<ElementNode>, IElement
	{
		public SparkElementWrapper(ElementNode node) : base(node)
		{
		}

		public IAttribute AddAttribute(string attributeName)
		{
			var attributeNode = new AttributeNode(attributeName, "");
			CurrentNode.Attributes.Add(attributeNode);
			return new SparkAttributeWrapper(attributeNode);
		}

		public bool HasAttribute(string attribute)
		{
			return GetAttributeByName(attribute)!=null;
		}

		private AttributeNode GetAttributeByName(string attribute)
		{
			return CurrentNode.Attributes.Where(x => AttributeNameEquals(x, attribute)).FirstOrDefault();
		}

		private static bool AttributeNameEquals(AttributeNode x, string attribute)
		{
			return x.Name.Equals(attribute, StringComparison.InvariantCultureIgnoreCase);
		}

		public IAttribute GetAttribute(string attribute)
		{
			AttributeNode attributeNode = GetAttributeByName(attribute);
			return new SparkAttributeWrapper(attributeNode);
		}
	}
}