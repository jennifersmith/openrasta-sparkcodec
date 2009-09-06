using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Transformers;
using Rhino.Mocks;

namespace OpenRasta.Codecs.Spark.UnitTests.Transformers
{
	[TestFixture]
	public class ElementTransformerTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ElementTransformerTestContext();
		}
		[Test]
		public void TransformCallsEachAction()
		{
			TestElement element = InternalTestNodes.TestElement("fred");
			GivenATransformerWithActions(element, MockRepository.GenerateStub<IElementTransformerAction>(), MockRepository.GenerateStub<IElementTransformerAction>());
			WhenTransformIsCalled();
			EachActionIsExecutedWithElement(element);
		}
		[Test]
		public void TransformReturnsTheElement()
		{
			TestElement element = InternalTestNodes.TestElement("fred");
			GivenATransformerWithActions(element);
			WhenTransformIsCalled();
			ThenTheResultIs(element);
		}

		private void ThenTheResultIs(IElement element)
		{
			Context.ElementResult.ShouldEqual(element);
		}

		private void EachActionIsExecutedWithElement(TestElement element)
		{
			Context.Target.GetActions().ForEach(x=>x.AssertWasCalled(y=>y.Do(element)));
		}

		private void WhenTransformIsCalled()
		{
			Context.ElementResult = Context.Target.Transform(null);
		}

		private void GivenATransformerWithActions(IElement forElement, params IElementTransformerAction[] actions)
		{
			Context.Target = new ElementTransformer( forElement, actions);
		}

		protected ElementTransformerTestContext Context { get; set; }

		public class ElementTransformerTestContext
		{
			public ElementTransformer Target { get; set; }

			public IElement ElementResult { get; set; }
		}
	}
}