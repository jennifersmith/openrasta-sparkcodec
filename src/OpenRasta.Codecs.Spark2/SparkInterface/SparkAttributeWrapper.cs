using System;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkAttributeWrapper : SparkNodeWrapper<AttributeNode>, IAttribute
	{
		public SparkAttributeWrapper(AttributeNode node) : base(node)
		{
		}

		public string Name
		{
			get
			{
				return CurrentNode.Name;
			}
		}

		public IConditionalExpressionNode AddConditionalExpressionNode()
		{
			var node = new ConditionNode();
			CurrentNode.Nodes.Add(node);
			return new SparkConditionNodeWrapper(node);
		}

		public string GetTextValue()
		{
			return CurrentNode.Value;
		}

		public bool Exists()
		{
			return CurrentNode != null;
		}
	}
}