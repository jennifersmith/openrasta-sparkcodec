using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class ConvertResourceToSelectedOptionActionTests
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			Context = new ConvertResourceToSelectedOptionsTestContext
			          	{
			          		Target = new ConvertResourceValueToSelectedOptionAction(new StubSyntaxProvider())
			          	};
		}

		#endregion

		private void ThenOptionChildElementShouldHaveConditionalSelectAttributeFor(string resourceValueExpr)
		{
			var expected = new ConditionalExpression(StubSyntaxProvider.GetTestNulLCheckAndEvalFor(resourceValueExpr),
			                                         resourceValueExpr);
			IAttribute attribute = Context.ElementTarget.Elements.Cast<TestElement>().First().GetAttribute("selected");
			attribute.ShouldBe<TestAttributeNode>();
			attribute.As<TestAttributeNode>().ConditionalExpressionNodes.First().ConditionalExpression.ShouldEqual(expected);
		}


		private void GivenElementTarget(TestElement element)
		{
			Context.ElementTarget = element;
		}

		private void ThenAttributeIsInsertedWithName(string attributeName)
		{
			Context.ElementTarget.Attributes.ShouldContain(x => x.Name == attributeName);
		}

		private void WhenActionCalledOnElement()
		{
			Context.Target.Do(Context.ElementTarget);
		}


		public ConvertResourceToSelectedOptionsTestContext Context { get; set; }

		public class ConvertResourceToSelectedOptionsTestContext
		{
			public ConvertResourceValueToSelectedOptionAction Target { get; set; }

			public TestElement ElementTarget { get; set; }
		}

		[Test]
		public void ShouldAddSelectedConditionalToEachOption()
		{
			GivenElementTarget(new TestElement("me").WithAttribute("for", "foo.bar").WithChildElement("option"));
			WhenActionCalledOnElement();
			ThenOptionChildElementShouldHaveConditionalSelectAttributeFor("foo.bar");
		}

		[Test]
		public void ShouldReplaceExistingAttribute()
		{
			GivenElementTarget(new TestElement("me").WithAttribute("for", "foo.bar").WithChildElement(new TestElement("option").WithAttribute("selected", "true")));
			WhenActionCalledOnElement();
			ThenOptionChildElementShouldHaveConditionalSelectAttributeFor("foo.bar");
		}
	}
}