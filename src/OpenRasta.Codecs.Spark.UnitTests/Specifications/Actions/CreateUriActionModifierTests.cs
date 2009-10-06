using System;
using System.Linq;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification.Actions;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	[TestFixture]
	public class CreateUriActionModifierTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new CreateUriActionModiferTestContext();
			Context.SyntaxProvider = new StubSyntaxProvider();
			Context.Target = new CreateUriActionModifier(Context.SyntaxProvider);
		}

		[Test]
		public void ShouldAddCreateUriExpressionToNewNodeFromOldNode()
		{
			GivenAnOriginalAttribute("original", "aValue");
			GivenANewAttribute("original");
			WhenAttributesAreModifed();
			ThenNewAttributeHasCreateUriValueFor("aValue" );
		}

		private void WhenAttributesAreModifed()
		{
			Context.Target.Modify(Context.OriginalAttribute, Context.NewAttribute);
		}

		private void GivenANewAttribute(string name)
		{
			Context.NewAttribute = new TestAttributeNode(name,"");
		}

		private void GivenAnOriginalAttribute(string name, string value)
		{
			Context.OriginalAttribute = new TestAttributeNode(name,value);
		}

		private void ThenNewAttributeHasCreateUriValueFor( string resource)
		{
			Context.NewAttribute.As<TestAttributeNode>().ShouldBeCreateUriExpressionFor(resource);
		}

		public CreateUriActionModiferTestContext Context { get; set; }

		public class CreateUriActionModiferTestContext
		{
			public	CreateUriActionModifier Target { get; set; }

			public TestElement ElementTarget { get; set; }

			public StubSyntaxProvider SyntaxProvider { get; set; }

			public IAttribute OriginalAttribute { get; set; }

			public IAttribute NewAttribute { get; set; }
		}
	}
}