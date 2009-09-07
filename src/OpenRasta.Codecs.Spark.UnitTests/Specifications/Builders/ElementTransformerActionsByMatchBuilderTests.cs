using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.UnitTests.Transformers;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Specification.Builders;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Builders
{
	[TestFixture]
	public class ElementTransformerActionsByMatchBuilderTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ElementTransformerActionsByMatchBuilderTestContext();
		}

		[Test]
		public void ShouldBuildInstanceWithGivenMatchers()
		{
			var matcher1 = new NodeMatcher("thisElement");
			var matcher2 = new NodeMatcher("thatElement");
			GivenATargetWithNodeMatchers(matcher1, matcher2);
			WhenBuilt();
			ResultShouldHaveMatchers(matcher1, matcher2);
		}
		[Test]
		public void ShouldBuildInstanceWithGivenActions()
		{
			var action1 = new StubElementTransformerAction();
			var action2 = new StubElementTransformerAction();
			GivenATarget();
			GivenAction(action1);
			GivenAction(action2);
			WhenBuilt();
			ResultShouldHaveActions(action1, action2);
		}

		private void ResultShouldHaveActions(params IElementTransformerAction[] actions)
		{
			Context.Result.ElementTransformerActions.ShouldEqual(actions);
		}

		private void GivenAction(IElementTransformerAction action)
		{
			Context.Target.AddAction(action);
		}

		private void GivenATarget()
		{
			Context.Target = new ElementTransformerActionsByMatchBuilder(new NodeMatcher[0]);
		}

		private void ResultShouldHaveMatchers(params NodeMatcher[] matchers)
		{
			Context.Result.NodeMatchers.ShouldEqual(matchers);
		}

		private void WhenBuilt()
		{
			Context.Result = Context.Target.Build();
		}

		private void GivenAnElementAction(StubElementTransformerAction elementTransformerAction)
		{
			Context.Target.AddAction(elementTransformerAction);
		}

		private void GivenATargetWithNodeMatchers(params NodeMatcher[] matchers)
		{
			Context.Target = new ElementTransformerActionsByMatchBuilder(matchers);
		}

		public ElementTransformerActionsByMatchBuilderTestContext Context { get; set; }

		public class ElementTransformerActionsByMatchBuilderTestContext
		{
			public ElementTransformerActionsByMatchBuilder Target { get; set; }

			public ElementTransformerActionsByMatch Result { get; set; }
		}
	}
}