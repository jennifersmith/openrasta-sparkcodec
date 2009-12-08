using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class ElementMatcherExtensionTests
	{
		#region Setup/Teardown

		[SetUp]
		public void SetUp()
		{
			Context = new ElementMatcherExtensionTestContext();
		}

		#endregion

		public ElementMatcherExtensionTestContext Context { get; private set; }

		private void ThenShouldBeAMatch()
		{
			Context.MatchResult.ShouldBeTrue();
		}

		private void ThenShouldNotBeAMatch()
		{
			Context.MatchResult.ShouldBeFalse();
		}

		private void WhenMatchAnyIsCalledFor(Tag element)
		{
			Context.MatchResult = Context.Tags.MatchesAtLeastOne(element);
		}


		private void GivenASetOfTags(params Tag[] tags)
		{
			Context.Tags = tags;
		}

		public class ElementMatcherExtensionTestContext
		{
			public IEnumerable<Tag> Tags { get; set; }

			public bool MatchResult { get; set; }
		}

		[Test]
		public void MatchAnyReturnsNegativeIfAtNonneOfTheNodeMatchersMatches()
		{
			GivenASetOfTags((new Tag("foo")), (new Tag("BAR")));
			WhenMatchAnyIsCalledFor(new Tag("neitherFooNorBar"));
			ThenShouldNotBeAMatch();
		}

		[Test]
		public void MatchAnyReturnsPositiveIfAtLeastOneOfTheNodeMatchersMatches()
		{
			GivenASetOfTags((new Tag("foo")), (new Tag("BAR")));
			WhenMatchAnyIsCalledFor(new Tag("foo"));
			ThenShouldBeAMatch();
		}
	}
}