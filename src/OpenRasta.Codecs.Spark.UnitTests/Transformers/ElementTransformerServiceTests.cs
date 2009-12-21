using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Transformers;

namespace OpenRasta.Codecs.Spark.UnitTests.Transformers
{
	[TestFixture]
	public class ElementTransformerServiceTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ElementTransformerServiceTestContext
			          	{
			          		ElementTransformerSpecification = new StubElementTransformerSpecification()
			          	};
			
			Context.Target = new ElementTransformerService(new StubSpecificationProvider().With(Context.ElementTransformerSpecification));
		}

		protected ElementTransformerServiceTestContext Context { get; set; }

		[Test]
		public void ShouldReturnAnElementTransformerContainingAllMatchesForCurrentNode()
		{
			Tag tag = new Tag("fred");
			var transformerAction = new StubElementTransformerAction();
			var transformerAction2 = new StubElementTransformerAction();

			GivenAnElementTarget(tag);
			GivenASpecification(tag, transformerAction, transformerAction2);
			WhenAnElementTransformerIsRequested();
			ThenTheElementTransformerShouldContainOnly(transformerAction, transformerAction2);
		}
		
		[Test]
		public void ShouldPlaceFinalElementTransformtionsAtEndOfList()
		{
			Tag tag = new Tag("fred");
			var transformerAction = new StubElementTransformerAction();
			var transformerAction2 = new StubElementTransformerAction();
			var finalAction1 = new StubElementTransformerAction();
			var finalAction2 = new StubElementTransformerAction();

			GivenAnElementTarget(tag);
			GivenASpecification(tag, transformerAction, transformerAction2);
			WhenAnElementTransformerIsRequested();
			ThenTheElementTransformerShouldContainOnly(transformerAction, transformerAction2, finalAction1, finalAction2);
		}

		[Test]
		public void IsTransformableShouldBeTrueIfAtLeastOneTransformAvailableForNode()
		{
			Tag tag = new Tag("fred");

			GivenAnElementTarget(tag);
			GivenASpecification(tag, new StubElementTransformerAction(), new StubElementTransformerAction());
			WhenIsTransformableIsCalled();
			ThenElementShouldBeTransformable();
		}

		[Test]
		public void IsTransformableShouldBeFalseIfNoTransformsAvailable()
		{
			Tag tag = new Tag("fred");
			GivenAnElementTarget(tag);
			WhenIsTransformableIsCalled();
			ThenElementShouldNotBeTransformable();
		}

		[Test]
		public void ShouldThrowIfNonTranformableElementRequestedForTransform()
		{
			Tag tag = new Tag("fred");
			GivenAnElementTarget(tag);
			ThenRequestingAnElementTransformShouldThrow();
		}

		private void ThenRequestingAnElementTransformShouldThrow()
		{
			var exception = Assert.Throws<ArgumentException>(() => Context.Target.GetTransformerFor(Context.Tag));
			exception.Message.ShouldEqual("Element is not transformable");
		}

		private void ThenElementShouldNotBeTransformable()
		{
			Context.IsTransformableResult.ShouldBeFalse();
		}

		private void ThenElementShouldBeTransformable()
		{
			Context.IsTransformableResult.ShouldBeTrue();
		}

		private void WhenIsTransformableIsCalled()
		{
			Context.IsTransformableResult = Context.Target.IsTransformable(Context.Tag);
		}

		private void ThenTheElementTransformerShouldContainOnly(params StubElementTransformerAction[] expectedActions)
		{
			Context.ElementTransformerResult.As<ElementTransformer>().GetActions().ShouldEqual(expectedActions);
		}


		private void WhenAnElementTransformerIsRequested()
		{
			Context.ElementTransformerResult = Context.Target.GetTransformerFor(Context.Tag);
		}

		private void GivenASpecification(Tag tag, params StubElementTransformerAction[] transformers)
		{
			Context.ElementTransformerSpecification =
				Context.ElementTransformerSpecification.WithActionForTag(tag, transformers);
		}

		private void GivenAnElementTarget(Tag element)
		{
			Context.Tag = element;
		}

		public class ElementTransformerServiceTestContext
		{
			public StubElementTransformerSpecification ElementTransformerSpecification { get; set; }

			public ElementTransformerService Target { get; set; }

			public Tag Tag { get; set; }

			public IElementTransformer ElementTransformerResult { get; set; }

			public bool IsTransformableResult { get; set; }
		}
	}


	public class StubElementTransformerSpecification : IElementTransformerSpecification
	{
		Dictionary<Tag, List<IElementTransformerAction>> _actionsByTag = new Dictionary<Tag, List<IElementTransformerAction>>();
		public StubElementTransformerSpecification WithActionForTag(Tag tag, params StubElementTransformerAction[] actions)
		{
			if (!_actionsByTag.ContainsKey(tag))
			{
				_actionsByTag[tag] = new List<IElementTransformerAction>();
			}
			_actionsByTag[tag].AddRange(actions);
			return this;
		}

		public IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<IElementTransformerAction> GetActionsForTag(Tag tag)
		{
			if (_actionsByTag.ContainsKey(tag))
			{
				return _actionsByTag[tag].ToArray();
			}
			return new IElementTransformerAction[0];
		}

	}

	public class StubSpecificationProvider : ISpecificationProvider
	{
		private IElementTransformerSpecification _elementTransformerSpecification;

		public StubSpecificationProvider With(IElementTransformerSpecification specification)
		{
			_elementTransformerSpecification = specification;
			return this;
		}

		public IElementTransformerSpecification CreateSpecification()
		{
			return _elementTransformerSpecification;
		}
	}

	public class StubElementTransformerAction : IElementTransformerAction
	{
		public void Do(IElement Element)
		{
		}
	}

}
