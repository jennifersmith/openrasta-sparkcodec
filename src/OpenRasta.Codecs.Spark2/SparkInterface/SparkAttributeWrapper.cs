using System;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Code;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkExpressionNodeWrapper : SparkNodeWrapper<ExpressionNode>, ICodeExpressionNode
	{
		public SparkExpressionNodeWrapper(ExpressionNode node) : base(node)
		{
		}

		public void SetExpressionBody(string expression)
		{
			CurrentNode.Code.Add(new Snippet(){Value = expression});
		}
	}
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

		public ICodeExpressionNode AddExpression()
		{
			ExpressionNode node = new ExpressionNode(string.Empty);
			CurrentNode.Nodes.Add(node);
			return new SparkExpressionNodeWrapper(node);
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