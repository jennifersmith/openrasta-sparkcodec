using OpenRasta.Codecs.Spark2.Matchers;

namespace OpenRasta.Codecs.Spark2.Specification
{
	public class SpecificationProvider : ISpecificationProvider
	{
		public IElementTransformerSpecification CreateSpecification()
		{
			return new ElementTransformerSpecification(new ElementTransformerActionsByMatch[0]);
		}
	}
}