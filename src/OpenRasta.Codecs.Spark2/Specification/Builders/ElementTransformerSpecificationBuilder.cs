using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Matchers;

namespace OpenRasta.Codecs.Spark2.Specification.Builders
{
	public class ElementTransformerSpecificationBuilder
	{
		private readonly List<IElementTransformerActionsByMatchBuilder> _builders = new List<IElementTransformerActionsByMatchBuilder>();

		public void AddTranformationBuilder(IElementTransformerActionsByMatchBuilder builder)
		{
			_builders.Add(builder);
		}	

		public IElementTransformerSpecification Build()
		{
			return new ElementTransformerSpecification(CreateMatchers());
		}

		private IEnumerable<ElementTransformerActionsByMatch> CreateMatchers()
		{
			return _builders.Select(x => x.Build());
		}
	}
}