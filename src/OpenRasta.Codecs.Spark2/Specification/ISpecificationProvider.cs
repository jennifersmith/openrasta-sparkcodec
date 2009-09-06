using System;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public interface ISpecificationProvider
	{
		IElementTransformerSpecification CreateSpecification();
	}
}