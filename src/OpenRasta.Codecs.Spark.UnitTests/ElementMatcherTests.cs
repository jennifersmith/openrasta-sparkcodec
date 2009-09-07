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
	public class ElementMatcherTests
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
			ThenTheResultShouldBe(ElementMatchResult.Match);
		}
		[Test]
		public void NodeMatcherShouldNotMatchWhenNamesAreDifferent()
		{
			GivenANodeMatcherForElementName("foo");
			WhenMatchedAgainstElement(InternalTestNodes.TestElement("BaR"));
			ThenTheResultShouldBe(ElementMatchResult.NoMatch);
		}

		private void ThenTheResultShouldBe(ElementMatchResult result)
		{
			Context.MatchResult.ShouldEqual(result);
		}

		private void WhenMatchedAgainstElement(IElement element)
		{
			Context.MatchResult = Context.ElementMatcher.Match(element);
		}

		private void GivenANodeMatcherForElementName(string foo)
		{
			Context.ElementMatcher = new NodeMatcher(foo);
		}

		protected NodeMatcherContextTests Context { get; set; }

		public class NodeMatcherContextTests
		{
			public NodeMatcher ElementMatcher { get; set; }

			public ElementMatchResult MatchResult { get; set; }
		}
	}
}
