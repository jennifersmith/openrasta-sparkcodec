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
		public void ShouldReturnActionsWhenOnlyOneNodeMatch()
		{
			const string currentElementName = "currentElement";
			TestElement element = InternalTestNodes.TestElement(currentElementName);
			var applicableAction1 = new StubElementTransformerAction();
			var applicableAction2 = new StubElementTransformerAction();

			GivenAMatcherAndAction(new NodeMatcher(currentElementName), applicableAction1, applicableAction2);
			GivenAMatcherAndAction(new NodeMatcher("anotherElement"), new StubElementTransformerAction(), new StubElementTransformerAction());
			WhenActionsRequestedFor(element);
			ReturnedActionsShouldbe(applicableAction1, applicableAction2);
		}
		[Test]
		public void ShouldReturnActionsWhenMoreThanOneNodeMatch()
		{
			const string currentElementName = "currentElement";
			TestElement element = InternalTestNodes.TestElement(currentElementName);
			var applicableAction1 = new StubElementTransformerAction();
			var applicableAction2 = new StubElementTransformerAction();

			GivenAMatcherAndAction(new NodeMatcher(currentElementName), applicableAction1);
			GivenAMatcherAndAction(new NodeMatcher(currentElementName), applicableAction2);
			WhenActionsRequestedFor(element);
			ReturnedActionsShouldbe(applicableAction1, applicableAction2);
		}
		[Test]
		public void ShouldReturnEmptyEnumerableIfNoMatches()
		{
			GivenAMatcherAndAction(new NodeMatcher("notTheCurrentElement"), new StubElementTransformerAction());
			WhenActionsRequestedFor(InternalTestNodes.TestElement("currentElement"));
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

		private void WhenActionsRequestedFor(IElement element)
		{
			Context.ResultActions =
				new ElementTransformerSpecification(Context.ElementTransformerActionsByMatch).GetActionsForElement(element);
		}

		private void GivenAMatcherAndAction(NodeMatcher matcher, params IElementTransformerAction[] actions)
		{
			Context.ElementTransformerActionsByMatch.Add(new ElementTransformerActionsByMatch(new []{matcher}, actions));
		}
		private void GivenTwoMatchersAndAction(NodeMatcher matcher1, NodeMatcher matcher2, params IElementTransformerAction[] actions)
		{
			Context.ElementTransformerActionsByMatch.Add(new ElementTransformerActionsByMatch(new[] { matcher1, matcher2 }, actions));
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
