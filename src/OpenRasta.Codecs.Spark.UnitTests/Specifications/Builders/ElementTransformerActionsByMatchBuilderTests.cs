using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.UnitTests.Transformers;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;
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
			var matcher1 = new Tag("thisElement");
			var matcher2 = new Tag("thatElement");
			GivenATargetWithTags(matcher1, matcher2);
			WhenBuilt();
			ResultShouldHaveTags(matcher1, matcher2);
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
		[Test]
		public void ShouldBuildInstanceWithGivenFinalActions()
		{
			var action1 = new StubElementTransformerAction();
			var action2 = new StubElementTransformerAction();
			var action3 = new StubElementTransformerAction();
			GivenATarget();
			GivenAction(action1);
			GivenAction(action2);
			GivenAFinalAction(action3);
			WhenBuilt();
			ResultShouldHaveActions(action1, action2);
			ResultShouldHaveFinalActions(action3);
		}

		[Test]
		public void ResultShouldNotHaveSameReferenceAsOriginal()
		{
			var action1 = new StubElementTransformerAction();
			var action2 = new StubElementTransformerAction();
			var action3 = new StubElementTransformerAction();
			GivenATarget();
			GivenAction(action1);
			GivenAction(action2);
			GivenAFinalAction(action3);
			WhenBuilt();
			ResultShouldNotBeReferenceEqualToTheOriginal();
		}

		private void ResultShouldNotBeReferenceEqualToTheOriginal()
		{
			(Context.Result.ElementTransformerActions == Context.Target.Actions).ShouldBeFalse();
			(Context.Result.FinalElementTransformerActions == Context.Target.FinalActions).ShouldBeFalse();
		}

		private void ResultShouldHaveFinalActions(params IElementTransformerAction[] elementTransformerActions)
		{
			Context.Result.FinalElementTransformerActions.ShouldEqual(elementTransformerActions);
		}

		private void GivenAFinalAction(StubElementTransformerAction elementTransformerAction)
		{
			Context.Target.AddFinalAction(elementTransformerAction);
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
			Context.Target = new ElementTransformerActionsByMatchBuilder(new Tag[0]);
		}

		private void ResultShouldHaveTags(params Tag[] tags)
		{
			Context.Result.Tags.ShouldEqual(tags);
		}

		private void WhenBuilt()
		{
			Context.Result = Context.Target.Build();
		}

		private void GivenAnElementAction(StubElementTransformerAction elementTransformerAction)
		{
			Context.Target.AddAction(elementTransformerAction);
		}

		private void GivenATargetWithTags(params Tag[] tags)
		{
			Context.Target = new ElementTransformerActionsByMatchBuilder(tags);
		}

		public ElementTransformerActionsByMatchBuilderTestContext Context { get; set; }

		public class ElementTransformerActionsByMatchBuilderTestContext
		{
			public ElementTransformerActionsByMatchBuilder Target { get; set; }

			public ElementTransformerActionsByMatch Result { get; set; }
		}
	}
}