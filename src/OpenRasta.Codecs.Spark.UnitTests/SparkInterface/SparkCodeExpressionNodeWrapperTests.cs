using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests.SparkInterface
{
	[TestFixture]
	public class SparkCodeExpressionNodeWrapperTests
	{
		public SparkCodeExpressionNodeWrapperTestContext Context { get; set; }
		[SetUp]
		public void SetUp()
		{
			Context = new SparkCodeExpressionNodeWrapperTestContext();	
		}

		[Test]
		public void AddCodeExpressionShouldAddCodeToTheUnderlyingNode()
		{
			string expression = "BLAH";

			GivenACodeExpressionNodeWrapping(SparkTestNodes.ExpressionNode());
			WhenCodeExpressionIsAdded(new CodeExpression(expression));
			ThenTheUnderlyingNodeShouldHaveExpression(expression);
		}

		private void ThenTheUnderlyingNodeShouldHaveExpression(string expression)
		{
			Context.Target.Unwrap().As<ExpressionNode>().Code.ToString().ShouldEqual(expression);
		}

		private void WhenCodeExpressionIsAdded(CodeExpression codeExpression)
		{
			Context.Target.SetExpressionBody(codeExpression);
		}

		private void GivenACodeExpressionNodeWrapping(ExpressionNode node)
		{
			Context.Target = new SparkCodeExpressionNodeWrapper(node);
		}

		public class SparkCodeExpressionNodeWrapperTestContext
		{
			public SparkCodeExpressionNodeWrapper Target { get; set; }
		}
	}
}