using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Code;
using Spark.Parser.Markup;
using System.Linq;

namespace OpenRasta.Codecs.Spark.UnitTests.SparkInterface
{
	[TestFixture]
	public class SparkAttributeWrapperTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new SparkAttributeWrapperTestContext();
		}

		[Test]
		public void AddCodeExpressionNodeAddsANewExpressionNodeToTheAttributeBody()
		{
			GivenAnAttribute(SparkTestNodes.BasicAttributeNode("attribute"));
			WhenACodeExpressionNodeIsAdded();
			ThenTheAttributeShouldContainACodeExpressionNode();
		}

		[Test]
		public void AddCodeExpressionNodeShouldReturnExpressionNodeWrapped()
		{
			AttributeNode attributeNode = SparkTestNodes.BasicAttributeNode("attribute");
			GivenAnAttribute(attributeNode);
			WhenACodeExpressionNodeIsAdded();
			CodeExpressionResultShouldWrap(attributeNode.Nodes.Last());
		}
		[Test]
		public void GetTextValueShouldReturnTextvalueOfWrappedAttribute()
		{
			GivenAnAttribute(SparkTestNodes.BasicAttributeNode("attribute").WithText("foo"));
			WhenGetTextCalled();
			ThenGetTextResultShouldEqual("foo");
		}

		[Test]
		public void ExistsShouldBeTrueIfWrapperInitialisedWithNonNullAttribute()
		{
			GivenAnAttribute(SparkTestNodes.BasicAttributeNode("attrib"));
			WhenAskedIfItExists();
			ExistsShouldBe(true);
		}
		[Test]
		public void ExistsShouldBeFalseIfWrapperInitialisedWithNullAttribute()
		{
			GivenAnAttribute(null);
			WhenAskedIfItExists();
			ExistsShouldBe(false);
		}


		[Test]
		public void NameShouldReturnTheNameOfTheWrappedNode()
		{
			GivenAnAttribute(SparkTestNodes.BasicAttributeNode("foo"));
			ThenNodeNameShouldBe("foo");
		}

		private void ThenNodeNameShouldBe(string nodeName)
		{
			Context.Target.Name.ShouldEqual(nodeName);
		}


		private void ExistsShouldBe(bool value)
		{
			Context.ExistsResult.ShouldEqual(value);
		}

		private void WhenAskedIfItExists()
		{
			Context.ExistsResult = Context.Target.Exists();
		}

		private void ThenGetTextResultShouldEqual(string foo)
		{
			Context.GetTextResult.ShouldEqual(foo);
		}

		private void WhenGetTextCalled()
		{
			Context.GetTextResult = Context.Target.GetTextValue();
		}

		private void CodeExpressionResultShouldWrap(Node node)
		{
			Context.AddCodeExpressionResult.Unwrap().ShouldEqual(node);
		}

		private void ThenTheAttributeShouldContainACodeExpressionNode()
		{
			Context.Target.Unwrap().As<AttributeNode>().Nodes.ShouldBeTrueForOne(x=>x is ExpressionNode);
		}

		private void WhenACodeExpressionNodeIsAdded()
		{
			Context.AddCodeExpressionResult = Context.Target.AddExpression();
		}

		private void GivenAnAttribute(AttributeNode node)
		{
			Context.Target = new SparkAttributeWrapper(node);
		}

		public SparkAttributeWrapperTestContext Context { get; set; }

		public class SparkAttributeWrapperTestContext
		{
			public SparkAttributeWrapper Target { get; set; }

			public ICodeExpressionNode AddCodeExpressionResult
			{
				get; set;
			}

			public string GetTextResult { get; set; }

			public bool ExistsResult { get; set; }
		}
	}
}