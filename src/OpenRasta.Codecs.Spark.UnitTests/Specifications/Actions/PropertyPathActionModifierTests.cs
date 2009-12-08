using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class PropertyPathActionModifierTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new PropertPathActionModiferTestContext {SyntaxProvider = new StubSyntaxProvider()};
			Context.Target = new PropertyPathActionModifier(Context.SyntaxProvider);
		}

		[Test]
		public void ShouldAddPropertyPathExpressionBasedOnOriginalAttribute()
		{
			GivenAnOriginalAttribute("original", "aValue");
			GivenANewAttribute("blah");
			WhenAttributesAreModifed();
			ThenNewAttributeHasCreateUriValueFor("aValue");
		}


		private void WhenAttributesAreModifed()
		{
			Context.Target.Modify(Context.OriginalAttribute, Context.NewAttribute);
		}

		private void GivenANewAttribute(string name)
		{
			Context.NewAttribute = new TestAttributeNode(name, "");
		}

		private void GivenAnOriginalAttribute(string name, string value)
		{
			Context.OriginalAttribute = new TestAttributeNode(name, value);
		}

		private void ThenNewAttributeHasCreateUriValueFor(string propertyAccessor)
		{
			Context.NewAttribute.As<TestAttributeNode>().ShouldBeGetPropertyPathExpressionFor(propertyAccessor);
		}

		public PropertPathActionModiferTestContext Context { get; set; }
		public class PropertPathActionModiferTestContext
		{
			public PropertyPathActionModifier Target { get; set; }

			public TestElement ElementTarget { get; set; }

			public StubSyntaxProvider SyntaxProvider { get; set; }

			public IAttribute OriginalAttribute { get; set; }

			public IAttribute NewAttribute { get; set; }
		}
	}
}