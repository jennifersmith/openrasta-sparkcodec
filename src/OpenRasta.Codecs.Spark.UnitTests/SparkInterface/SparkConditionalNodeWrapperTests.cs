using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests.SparkInterface
{
	[TestFixture]
	public class SparkConditionalNodeWrapperTests
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			Context = new SparkConditionalNodeWrapperTestContext();
		}

		#endregion

		private void TheExpressionbodyShouldContainExpression(string expression)
		{
			var conditionNode = Context.Target.Unwrap().As<ConditionNode>();
			conditionNode.Nodes.Count.ShouldEqual(1);
			var node = conditionNode.Nodes[0].As<ExpressionNode>();
			node.Code.Count.ShouldEqual(1);
			node.Code[0].Value.ShouldEqual(expression);
		}

		private void TheExpressionBodyShouldBeConditionalWithCondition(string condition)
		{
			var expressionNode = Context.Target.Unwrap().As<ConditionNode>();
			expressionNode.Code.Count.ShouldEqual(1);
			expressionNode.Code[0].Value.ShouldEqual(condition);
		}

		private void WhenSetExpressionBodyIsCalledWith(string conditional, string code)
		{
			Context.Target.SetExpressionBody(new ConditionalExpression(conditional, code));
		}

		private void GivenAnExpressionNode(ConditionNode node)
		{
			Context.Target = new SparkConditionNodeWrapper(node);
		}
		public SparkConditionalNodeWrapperTestContext Context { get; set; }

		public class SparkConditionalNodeWrapperTestContext
		{
			public SparkConditionNodeWrapper Target { get; set; }
		}

		[Test]
		public void SetExpressionBodyShouldSetConditionalCorrectly()
		{
			GivenAnExpressionNode(SparkTestNodes.ConditionNode());
			WhenSetExpressionBodyIsCalledWith("someCondition", "someCode");
			TheExpressionBodyShouldBeConditionalWithCondition("someCondition");
			TheExpressionbodyShouldContainExpression("someCode");
		}
	}
}