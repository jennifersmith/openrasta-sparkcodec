using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class ElementMatcherExtensionTests
	{
		public NodeMatcherSetContext Context { get; private set; }
		[SetUp]
		public void SetUp()
		{
			Context = new NodeMatcherSetContext();
		}
		[Test]
		public void MatchAnyReturnsPositiveIfAtLeastOneOfTheNodeMatchersMatches()
		{
			GivenASetOfNodeMatchers(new NodeMatcher("foo"), new NodeMatcher("BAR"));
			WhenMatchAnyIsCalledFor(InternalTestNodes.TestElement("foo"));
			ThenMatchResultShouldBe(ElementMatchResult.Match);
		}
		[Test]
		public void MatchAnyReturnsNegativeIfAtNonneOfTheNodeMatchersMatches()
		{
			GivenASetOfNodeMatchers(new NodeMatcher("foo"), new NodeMatcher("BAR"));
			WhenMatchAnyIsCalledFor(InternalTestNodes.TestElement("neitherFooNorBar"));
			ThenMatchResultShouldBe(ElementMatchResult.NoMatch);
		}

		private void ThenMatchResultShouldBe(ElementMatchResult result)
		{
			Context.MatchResult.ShouldEqual(result);
		}

		private void WhenMatchAnyIsCalledFor(IElement element)
		{
			Context.MatchResult = Context.ElementMatchers.MatchesAtLeastOne(element);
		}


		private void GivenASetOfNodeMatchers(params NodeMatcher[] nodeMatchers)
		{
			Context.ElementMatchers = nodeMatchers;
		}

		public class NodeMatcherSetContext
		{
			public IEnumerable<NodeMatcher> ElementMatchers { get; set; }

			public ElementMatchResult MatchResult { get; set; }
		}
	}
}