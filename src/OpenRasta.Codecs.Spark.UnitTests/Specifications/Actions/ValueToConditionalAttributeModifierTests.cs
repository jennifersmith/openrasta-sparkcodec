using System;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class ValueToConditionalAttributeModifierTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ValueToConditionalAttributeModifierTestContext
			          	{
			          		SyntaxProvider = new StubSyntaxProvider(),
							NewAttribute = new TestAttributeNode("new", "")
			          	};
			Context.Target = new ValueToConditionalAttribute(new StubSyntaxProvider());
		}
		[Test]
		public void ShouldAddCodeNodeWithTrueLiteral()
		{
			GivenAnOriginalAttribute("foo", "blah");
			WhenAttributesAreModified();
			ThenNewAttributeShouldHaveCodeNode("true");
		}
		[Test]
		public void ShouldAddConditionalNodeWithNullCheckedResourceValue()
		{
			GivenAnOriginalAttribute("foo", "blah");
			WhenAttributesAreModified();
			ThenNewAttributeShouldHaveConditionalNodeWithNullCheckFor("blah");
		}
		[Test]
		public void ConditionalShouldAppearAfterLiteral()
		{
			GivenAnOriginalAttribute("foo", "blah");
			WhenAttributesAreModified();
			NewAttributeShouldHaveCodeNodeFollowedByConditional();
		}

		private void NewAttributeShouldHaveCodeNodeFollowedByConditional()
		{
			Context.NewAttribute.As<TestAttributeNode>().Nodes.ShouldHaveCount(2);
			Context.NewAttribute.As<TestAttributeNode>().Nodes.First().ShouldBe<TestCodeExpressionNode>();
			Context.NewAttribute.As<TestAttributeNode>().Nodes.Skip(1).First().ShouldBe<TestConditionalExpressionNode>();
		}

		private void ThenNewAttributeShouldHaveConditionalNodeWithNullCheckFor(string value)
		{
			ConditionalExpression expected = new ConditionalExpression(StubSyntaxProvider.GetTestNulLCheckAndEvalFor(value), value);
			Context.NewAttribute.As<TestAttributeNode>().ConditionalExpressionNodes.Single().As<TestConditionalExpressionNode>().ConditionalExpression.ShouldEqual(expected);
		}

		private void ThenNewAttributeShouldHaveCodeNode(string value)
		{
			Context.NewAttribute.As<TestAttributeNode>().CodeNodes.Single().As<TestCodeExpressionNode>().CodeExpression.ShouldEqual(new CodeExpression(value));
		}

		private void WhenAttributesAreModified()
		{
			Context.Target.Modify(Context.OriginalAttribute, Context.NewAttribute);
		}

		private void GivenAnOriginalAttribute(string foo, string bla)
		{
			Context.OriginalAttribute = new TestAttributeNode(foo, bla);
		}

		public ValueToConditionalAttributeModifierTestContext Context { get; set; }
		public class ValueToConditionalAttributeModifierTestContext
		{
			public ValueToConditionalAttribute Target { get; set; }

			public StubSyntaxProvider SyntaxProvider { get; set; }

			public IAttribute OriginalAttribute { get; set; }

			public IAttribute NewAttribute { get; set; }
		}
	}
}
