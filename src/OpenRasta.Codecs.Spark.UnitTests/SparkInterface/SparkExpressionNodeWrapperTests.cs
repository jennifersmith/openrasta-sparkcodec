using System;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests.SparkInterface
{
	public class TestSparkNodeWrapper : SparkNodeWrapper<ElementNode>
	{
		public TestSparkNodeWrapper(ElementNode node)
			: base(node)
		{
		}
	}

	[TestFixture]
	public class SparkExpressionNodeWrapperTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new SparkExpressionNodeWrapperTestContext();
		}
		[Test]
		public void SetExpressionBodyShouldSetBody()
		{
			GivenAnExpressionNode(SparkTestNodes.ExpressionNode());
			WhenSetExpressionBodyIsCalledWith("someCode");
			TheExpressionBodyShouldContain("someCode");
		}

		private void TheExpressionBodyShouldContain(string somecode)
		{
			Context.Target.Unwrap().As<ExpressionNode>().Code.Last().Value.ShouldEqual(somecode);
		}

		private void WhenSetExpressionBodyIsCalledWith(string somecode)
		{
			Context.Target.SetExpressionBody(somecode);
		}

		private void GivenAnExpressionNode(ExpressionNode node)
		{
			throw new NotImplementedException();
		}

		public SparkExpressionNodeWrapperTestContext Context { get; set; }

		public class SparkExpressionNodeWrapperTestContext
		{
			public SparkExpressionNodeWrapper Target { get; set; }
		}
	}
}