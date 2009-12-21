using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;
using Rhino.Mocks;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class RemoveAttributeActionTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new RemoveAttributeActionTestContext();
		}
		[Test]
		public void ShouldRemoveTargetAttributeIfPresent()
		{
			GivenATargetAttribute("removeMe");
			WhenActionPerformedOnElement(new TestElement("foo").WithAttribute("removeMe", "seriously"));
			ThenElementShouldNotHaveAttribute("removeMe");
		}
		[Test]
		public void ShouldNotChangeTheElementIfDoesntHaveAttribute()
		{
			GivenATargetAttribute("removeMe");
			WhenActionPerformedOnElement(new TestElement("foo").WithAttribute("dontRemoveMe", "seriously"));
			ThenElementShouldStillHaveAttribute("dontRemoveMe");
		}

		private void ThenElementShouldStillHaveAttribute(string attributeName)
		{
			Context.TargetElement.Attributes.ShouldContain(x => x.Name.ToUpperInvariant() == attributeName.ToUpperInvariant());
		}

		private void ThenElementShouldNotHaveAttribute(string attributeName)
		{
			Context.TargetElement.Attributes.ShouldNotContain(x=>x.Name.ToUpperInvariant()==attributeName.ToUpperInvariant());
		}

		private void WhenActionPerformedOnElement(TestElement element)
		{
			Context.TargetElement = element;
			Context.Target.Do(element);
		}

		private void GivenATargetAttribute(string attributeName)
		{
			Context.Target = new RemoveAttributeAction(attributeName);
		}

		public RemoveAttributeActionTestContext Context { get; set; }

		public class RemoveAttributeActionTestContext
		{
			public RemoveAttributeAction Target { get; set; }

			public TestElement ElementTarget { get; set; }

			public TestElement TargetElement { get; set; }
		}
	}
}