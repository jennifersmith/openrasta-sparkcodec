using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class NodeMatcherExtensionsTests
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
			ThenMatchResultShouldBe(NodeMatchResult.Match);
		}
		[Test]
		public void MatchAnyReturnsNegativeIfAtNonneOfTheNodeMatchersMatches()
		{
			GivenASetOfNodeMatchers(new NodeMatcher("foo"), new NodeMatcher("BAR"));
			WhenMatchAnyIsCalledFor(InternalTestNodes.TestElement("neitherFooNorBar"));
			ThenMatchResultShouldBe(NodeMatchResult.NoMatch);
		}

		private void ThenMatchResultShouldBe(NodeMatchResult result)
		{
			Context.MatchResult.ShouldEqual(result);
		}

		private void WhenMatchAnyIsCalledFor(INode element)
		{
			Context.MatchResult = Context.NodeMatchers.MatchesAtLeastOne(element);
		}


		private void GivenASetOfNodeMatchers(params NodeMatcher[] nodeMatchers)
		{
			Context.NodeMatchers = nodeMatchers;
		}

		public class NodeMatcherSetContext
		{
			public IEnumerable<NodeMatcher> NodeMatchers { get; set; }

			public NodeMatchResult MatchResult { get; set; }
		}
	}
}