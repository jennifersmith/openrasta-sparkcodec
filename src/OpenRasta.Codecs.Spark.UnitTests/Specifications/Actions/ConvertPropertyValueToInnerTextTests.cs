using System;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;
using Rhino.Mocks;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class ConvertPropertyValueToInnerTextTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ConvertPropertyValueToInnerTextTestContext()
			          	{
							Target = new ConvertPropertyValueToInnerText(new StubSyntaxProvider())
			          	};
		}

		[Test]
		public void ShouldAddInnerTextWithResourceExpression()
		{
			TestElement element = InternalTestNodes.TestElement("foo").WithAttribute("for", "resource.Stuff");
			element.AddTextElement("this is the inner text");
			GivenElementTarget(element);
			WhenActionCalledOnElement();
			ThenThenElementShouldContainConditional("resource.Stuff", StubSyntaxProvider.GetTestNullCheckExpression("resource"));
		}
		[Test]
		public void ShouldRemoveInnerText()
		{
			TestElement element = InternalTestNodes.TestElement("foo").WithAttribute("for", "resource.Stuff");
			element.AddTextElement("this is the inner text");
			GivenElementTarget(element);
			WhenActionCalledOnElement();
			ThenThenElementShouldContainNoInnerTextNodes();
		}
		[Test]
		public void ShouldNotReplaceInnerTextIfHasNoForAttribute()
		{
			TestElement element = InternalTestNodes.TestElement("foo");
			element.AddTextElement("this is the inner text");
			GivenElementTarget(element);
			WhenActionCalledOnElement();
			ThenTheInnerTextShouldBe("this is the inner text");
		}

		private void ThenThenElementShouldContainNoInnerTextNodes()
		{
			Context.ElementTarget.As<TestElement>().TextNodes.ShouldBeEmpty();
		}

		private void ThenThenElementShouldContainConditional(string snippet, string condition)
		{
			Context.ElementTarget.As<TestElement>().ConditionalExpressionNodes.First().As<TestConditionalExpressionNode>().ConditionalExpression.ShouldEqual(new ConditionalExpression(condition,snippet));
		}

		private void ThenTheInnerTextShouldBe(string expectedInnerText)
		{
			Context.ElementTarget.As<TestElement>().InnerText().ShouldEqual(expectedInnerText);
		}


		private void GivenElementTarget(TestElement element)
		{
			Context.ElementTarget = element;
		}


		private void WhenActionCalledOnElement()
		{
			Context.Target.Do(Context.ElementTarget);
		}

	
		public ConvertPropertyValueToInnerTextTestContext Context { get; set; }

		public class ConvertPropertyValueToInnerTextTestContext
		{
			public ConvertPropertyValueToInnerText Target { get; set; }

			public TestElement ElementTarget { get; set; }

		}
	}
}