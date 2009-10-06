using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;
using Rhino.Mocks;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class ConvertAttributeActionTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ConvertAttributeToActionTestContext
			          	{
			          		AttributeModifier = MockRepository.GenerateStub<IAttributeModifer>()
			          	};
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
		public void ShouldCallAttributeModiferToTransformAttributeToTheNewValue()
		{
			TestElement element = InternalTestNodes.TestElement("").WithAttribute("from", "aValue");
			GivenToAndFromAttributes("to", "from");
			GivenElementTarget(element);
			WhenActionCalledOnElement();	
			ThenHasCalledModiferWithAttributes(element.GetAttribute("from"), element.GetAttribute("to"));
		}

		private void ThenHasCalledModiferWithAttributes(IAttribute originalAttribute, IAttribute newAttribute)
		{
			Context.AttributeModifier.AssertWasCalled(x => x.Modify(originalAttribute, newAttribute));
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
			Context.Target = new ConvertAttributeAction(toAttribute, fromAttribute, Context.AttributeModifier);
		}

		public ConvertAttributeToActionTestContext Context { get; set; }

		public class ConvertAttributeToActionTestContext
		{
			public ConvertAttributeAction Target { get; set; }

			public TestElement ElementTarget { get; set; }


			public IAttributeModifer AttributeModifier { get; set; }
		}
	}
}