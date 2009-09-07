using OpenRasta.Codecs.Spark2.Specification.Builders;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public class Scratch : ISpecificationProvider
	{
		public IElementTransformerSpecification CreateSpecification()
		{
			var builder = new ElementTransformerSpecificationBuilder();

			builder.MatchAll(Tag.AnchorTag)
				.Do(
				Convert.ToAttributeToHref(),
				Convert.ToTypeAttributeToHref()
				);
			builder.MatchAll(Tag.FormTag)
				.Do(
				Convert.ToAttributeToHref(),
				Convert.ToTypeAttributeToHref()
				);
			builder.MatchAll(Tag.AllInputTags)
				.Do(
				Convert.ToAttributeToHref(),
				Convert.ToTypeAttributeToHref()
				);
			return builder.Build();
		}
	}

}