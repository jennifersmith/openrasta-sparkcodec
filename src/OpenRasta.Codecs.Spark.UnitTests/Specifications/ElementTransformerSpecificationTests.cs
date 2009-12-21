using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.UnitTests.Transformers;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications
{
	[TestFixture]
	public class ElementTransformerSpecificationTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ElementTransformerSpecificationTestContext();
		}

		protected ElementTransformerSpecificationTestContext Context { get; set; }

		[Test]
		public void ShouldReturnActionsWhenOnlyOneTagMatch()
		{
			Tag currentTag = new Tag("current");
			Tag anotherTag = new Tag("foobar123");
			var applicableAction1 = new StubElementTransformerAction();
			var applicableAction2 = new StubElementTransformerAction();

			GivenATagAndAction(currentTag, applicableAction1, applicableAction2);
			GivenATagAndAction(anotherTag, new StubElementTransformerAction(), new StubElementTransformerAction());
			WhenActionsRequestedFor(currentTag);
			ReturnedActionsShouldbe(applicableAction1, applicableAction2);
		}
		[Test]
		public void ShouldReturnActionsWhenMoreThanOneNodeMatch()
		{
			Tag currentTag = new Tag("currentTag");
			var applicableAction1 = new StubElementTransformerAction();
			var applicableAction2 = new StubElementTransformerAction();

			GivenATagAndAction(currentTag, applicableAction1);
			GivenATagAndAction(currentTag, applicableAction2);
			WhenActionsRequestedFor(currentTag);
			ReturnedActionsShouldbe(applicableAction1, applicableAction2);
		}

		[Test]
		public void ShouldPlaceFinalElementTransformtionsAtEndOfList()
		{
			Tag currentTag = new Tag("currentTag");
			var applicableAction1 = new StubElementTransformerAction();
			var applicableAction2 = new StubElementTransformerAction();
			var finalAction1 = new StubElementTransformerAction();
			var finalAction2 = new StubElementTransformerAction();

			GivenATagActionAndFinal(currentTag, new[] { applicableAction1 }, new[] { finalAction1 });
			GivenATagActionAndFinal(currentTag, new[] { applicableAction2 }, new[] { finalAction2 });
			WhenActionsRequestedFor(currentTag);
			ReturnedActionsShouldbe(applicableAction1, applicableAction2, finalAction1, finalAction2);

		}

		[Test]
		public void ShouldReturnEmptyEnumerableIfNoMatches()
		{
			GivenATagAndAction(new Tag("notTheCurrentElement"), new StubElementTransformerAction());
			WhenActionsRequestedFor(new Tag("theCurrentElement"));
			ReturnedActionsShouldbeEmpty();
		}

		private void ReturnedActionsShouldbeEmpty()
		{
			Context.ResultActions.ShouldBeEmpty();
		}

		private void ReturnedActionsShouldbe(params StubElementTransformerAction[] actions)
		{
			Context.ResultActions.ShouldEqual(actions);
		}

		private void WhenActionsRequestedFor(Tag tag)
		{
			Context.ResultActions =
				new ElementTransformerSpecification(Context.ElementTransformerActionsByMatch).GetActionsForTag(tag);
		}

		private void GivenATagAndAction(Tag tag, params IElementTransformerAction[] actions)
		{
			Context.ElementTransformerActionsByMatch.Add(new ElementTransformerActionsByMatch(new[] { tag }, actions, new[]
			                                                                       	{
			                                                                       		new StubElementTransformerAction()
			                                                                       	}));
		}
		private void GivenATagActionAndFinal(Tag tag, IEnumerable<IElementTransformerAction> actions, IEnumerable<IElementTransformerAction> finalActions)
		{
			Context.ElementTransformerActionsByMatch.Add(new ElementTransformerActionsByMatch(new[] { tag }, actions, finalActions));
		}
		private void GivenTwoTagsAndActions(Tag tag1, Tag tag2, params IElementTransformerAction[] actions)
		{
			Context.ElementTransformerActionsByMatch.Add(new ElementTransformerActionsByMatch(new[] { tag1, tag2 }, actions, new[]
			                                                                       	{
			                                                                       		new StubElementTransformerAction()
			                                                                       	}));
		}
		public class ElementTransformerSpecificationTestContext
		{
			private IList<ElementTransformerActionsByMatch> _elementTransformerActionsByMatch = new List<ElementTransformerActionsByMatch>();

			public IList<ElementTransformerActionsByMatch> ElementTransformerActionsByMatch
			{
				get { return _elementTransformerActionsByMatch; }
			}

			public IEnumerable<IElementTransformerAction> ResultActions { get; set; }
		}
	}
}
