using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	[TestFixture]
	public class NodeMatcherTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new NodeMatcherContextTests();
		}
		[Test]
		public void NodeMatcherShouldMatchOnCaseInsensitiveElementName()
		{
			GivenANodeMatcherForElementName("foo");
			WhenMatchedAgainstElement(InternalTestNodes.TestElement("FOo"));
			ThenTheResultShouldBe(NodeMatchResult.Match);
		}
		[Test]
		public void NodeMatcherShouldNotMatchWhenNamesAreDifferent()
		{
			GivenANodeMatcherForElementName("foo");
			WhenMatchedAgainstElement(InternalTestNodes.TestElement("BaR"));
			ThenTheResultShouldBe(NodeMatchResult.NoMatch);
		}

		private void ThenTheResultShouldBe(NodeMatchResult result)
		{
			Context.MatchResult.ShouldEqual(result);
		}

		private void WhenMatchedAgainstElement(INode element)
		{
			Context.MatchResult = Context.NodeMatcher.Match(element);
		}

		private void GivenANodeMatcherForElementName(string foo)
		{
			Context.NodeMatcher = new NodeMatcher(foo);
		}

		protected NodeMatcherContextTests Context { get; set; }

		public class NodeMatcherContextTests
		{
			public NodeMatcher NodeMatcher { get; set; }

			public NodeMatchResult MatchResult { get; set; }
		}
	}
}
