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
			TestElement element = InternalTestNodes.TestElement("fred");
			var transformerAction = new StubElementTransformerAction();
			var transformerAction2 = new StubElementTransformerAction();

			GivenAnElementTarget(element);
			GivenASpecification(element, transformerAction, transformerAction2);
			WhenAnElementTransformerIsRequested();
			ThenTheElementTransformerShouldContainOnly(transformerAction, transformerAction2);
		}

		[Test]
		public void IsTransformableShouldBeTrueIfAtLeastOneTransformAvailableForNode()
		{
			TestElement element = InternalTestNodes.TestElement("fred");

			GivenAnElementTarget(element);
			GivenASpecification(element, new StubElementTransformerAction(), new StubElementTransformerAction());
			WhenIsTransformableIsCalled();
			ThenElementShouldBeTransformable();
		}

		[Test]
		public void IsTransformableShouldBeFalseIfNoTransformsAvailable()
		{
			TestElement element = InternalTestNodes.TestElement("fred");

			GivenAnElementTarget(element);
			WhenIsTransformableIsCalled();
			ThenElementShouldNotBeTransformable();
		}

		[Test]
		public void ShouldThrowIfNonTranformableElementRequestedForTransform()
		{
			TestElement element = InternalTestNodes.TestElement("fred");

			GivenAnElementTarget(element);
			ThenRequestingAnElementTransformShouldThrow();
		}

		private void ThenRequestingAnElementTransformShouldThrow()
		{
			var exception = Assert.Throws<ArgumentException>(() => Context.Target.GetTransformerFor(Context.ElementTarget));
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
			Context.IsTransformableResult = Context.Target.IsTransformable(Context.ElementTarget);
		}

		private void ThenTheElementTransformerShouldContainOnly(params StubElementTransformerAction[] expectedActions)
		{
			Context.ElementTransformerResult.As<ElementTransformer>().GetActions().ShouldEqual(expectedActions);
		}


		private void WhenAnElementTransformerIsRequested()
		{
			Context.ElementTransformerResult = Context.Target.GetTransformerFor(Context.ElementTarget);
		}

		private void GivenASpecification(IElement element, params StubElementTransformerAction[] transformers)
		{
			Context.ElementTransformerSpecification =
				Context.ElementTransformerSpecification.WithActionForElement(element, transformers);
		}

		private void GivenAnElementTarget(TestElement element)
		{
			Context.ElementTarget = element;
		}

		public class ElementTransformerServiceTestContext
		{
			public StubElementTransformerSpecification ElementTransformerSpecification { get; set; }

			public ElementTransformerService Target { get; set; }

			public TestElement ElementTarget { get; set; }

			public IElementTransformer ElementTransformerResult { get; set; }

			public bool IsTransformableResult { get; set; }
		}
	}


	public class StubElementTransformerSpecification : IElementTransformerSpecification
	{
		Dictionary<IElement, List<IElementTransformerAction>> _actionsByElement = new Dictionary<IElement, List<IElementTransformerAction>>();
		public StubElementTransformerSpecification WithActionForElement(IElement element, params StubElementTransformerAction[] actions )
		{
			if(!_actionsByElement.ContainsKey(element))
			{
				_actionsByElement[element] = new List<IElementTransformerAction>();
			}
			_actionsByElement[element].AddRange(actions);
			return this;
		}
		public IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element)
		{
			if(_actionsByElement.ContainsKey(element))
			{
				return _actionsByElement[element].ToArray();
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
