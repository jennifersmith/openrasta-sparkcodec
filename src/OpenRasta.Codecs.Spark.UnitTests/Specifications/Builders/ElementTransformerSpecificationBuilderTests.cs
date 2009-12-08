
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.UnitTests.Transformers;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Specification.Builders;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Builders
{
	[TestFixture]
	public class ElementTransformerSpecificationBuilderTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new ElementTransformerSpecificationBuilderTestContext
			          	{
			          		Target = new ElementTransformerSpecificationBuilder()
			          	};
		}

		protected ElementTransformerSpecificationBuilderTestContext Context { get; set; }

		[Test]
		public void ShouldCreateAnElementTransformerSpecificationWhenBuildCalled()
		{
			ElementTransformerActionsByMatch elementTransformerActionsByMatch = CreateElementTransformActionMatch();
			ElementTransformerActionsByMatch elementTransformerActionsByMatch2 = CreateElementTransformActionMatch();

			GivenAnElementTransformationMatcher(elementTransformerActionsByMatch);
			GivenAnElementTransformationMatcher(elementTransformerActionsByMatch2);
			WhenSpecificationIsBuilt();
			ThenTheSpecificationShouldConain(elementTransformerActionsByMatch, elementTransformerActionsByMatch2);
		}

		private void ThenTheSpecificationShouldConain(params ElementTransformerActionsByMatch[] matches)
		{
			Context.Result.As<ElementTransformerSpecification>().GetElementTransformerActionsByMatch().ShouldEqual(matches);
		}

		private void WhenSpecificationIsBuilt()
		{
			Context.Result = Context.Target.Build();
		}

		private static ElementTransformerActionsByMatch CreateElementTransformActionMatch()
		{
			return new ElementTransformerActionsByMatch(new[] {new Tag("AMatch")}, new[]
			                                                                               	{
			                                                                               		new StubElementTransformerAction()
			                                                                               	});
		}

		private void GivenAnElementTransformationMatcher(ElementTransformerActionsByMatch elementTransformerActionsByMatch)
		{
			Context.Target.AddTranformationBuilder(
				new StubElementTransformerActionsByMatchBuilder().With(
					elementTransformerActionsByMatch));
		}


		public class ElementTransformerSpecificationBuilderTestContext
		{
			public ElementTransformerSpecificationBuilder Target { get; set; }

			public IElementTransformerSpecification Result { get; set; }
		}
	}

	public class StubElementTransformerActionsByMatchBuilder : IElementTransformerActionsByMatchBuilder
	{
		private ElementTransformerActionsByMatch _elementTransformerActionsByMatch;

		public StubElementTransformerActionsByMatchBuilder With(ElementTransformerActionsByMatch elementTransformerActionsByMatch)
		{
			this._elementTransformerActionsByMatch = elementTransformerActionsByMatch;
			return this;
		}

		public void AddAction(IElementTransformerAction ElementTransformerAction)
		{
			throw new NotImplementedException();
		}

		public ElementTransformerActionsByMatch Build()
		{
			return _elementTransformerActionsByMatch;
		}
	}
}
