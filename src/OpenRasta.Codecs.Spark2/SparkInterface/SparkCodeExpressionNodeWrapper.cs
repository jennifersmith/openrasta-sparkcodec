using System;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Code;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkCodeExpressionNodeWrapper :SparkNodeWrapper<ExpressionNode>, ICodeExpressionNode
	{
		public SparkCodeExpressionNodeWrapper(ExpressionNode node) : base(node)
		{
		}

		public void SetExpressionBody(CodeExpression expression)
		{
			CurrentNode.Code.Add(new Snippet(){Value = expression.Render()});
		}
	}
}