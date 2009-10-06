using System;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Code;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkConditionNodeWrapper : SparkNodeWrapper<ConditionNode>, IConditionalExpressionNodeWrapper
	{
		public SparkConditionNodeWrapper(ConditionNode node) : base(node)
		{
		}

		public void SetExpressionBody(ConditionalExpression conditionalExpression)
		{
			CurrentNode.Code = new Snippets(conditionalExpression.Condition);
			CurrentNode.Nodes.Add(new ExpressionNode(conditionalExpression.Expression));
		}

		public void SetExpressionBody(CodeExpression codeExpression)
		{
			throw new NotImplementedException();
		}
	}
}