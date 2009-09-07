using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Specification.Actions;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class ConvertAttributeToUriActionTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ConvertAttributeToUriActionTestContext();
			Context.SyntaxProvider = new StubSyntaxProvider();
		}

		[Test]
		public void ShouldAddAnAttributeOfTheSpecifiedNameWhenActionIsCalledAndFromAttributeExists()
		{
			TestElement element = InternalTestNodes.TestElement("").WithAttribute("from", "aValue");
			GivenToAndFromAttributes("to", "from");
			GivenElementTarget(element);
			WhenActionCalledOnElement();
			ThenAttributeIsInsertedWithName("to");
		}

		[Test]
		public void ShouldNotAddToAttributeIfTheFromAttributeDoesntExist()
		{
			TestElement element = InternalTestNodes.TestElement("").WithAttribute("NOTTHEFROMATTRIBUTE", "aValue");
			GivenToAndFromAttributes("to", "from");
			GivenElementTarget(element);
			WhenActionCalledOnElement();
			ThenAttributeWithNameIsNotInserted("to");
		}

		[Test]
		public void ShouldAddToAttributeWithCreateUriCodeExpression()
		{
			TestElement element = InternalTestNodes.TestElement("").WithAttribute("from", "aValue");
			GivenToAndFromAttributes("to", "from");
			GivenElementTarget(element);
			WhenActionCalledOnElement();
			ThenHasCreateUriCodeAttribute("to", "aValue" );
		}

		private void ThenHasCreateUriCodeAttribute(string attributeName, string resource)
		{
			Context.ElementTarget.Attributes.Where(x=>x.Name==attributeName).First().As<TestAttributeNode>().ShouldBeCreateUriExpressionFor(resource);
		}

		private void ThenAttributeWithNameIsNotInserted(string attributeName)
		{
			Context.ElementTarget.Attributes.ShouldNotContain(x => x.Name == attributeName);
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

		private void GivenToAndFromAttributes(string toAttribute, string fromAttribute)
		{
			Context.Target = new ConvertAttributeToUriAction(toAttribute, fromAttribute, Context.SyntaxProvider);
		}

		public ConvertAttributeToUriActionTestContext Context { get; set; }

		public class ConvertAttributeToUriActionTestContext
		{
			public ConvertAttributeToUriAction Target { get; set; }

			public TestElement ElementTarget { get; set; }

			public StubSyntaxProvider SyntaxProvider { get; set; }
		}
	}
}