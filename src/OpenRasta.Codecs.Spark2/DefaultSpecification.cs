using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Specification.Builders;
using OpenRasta.Codecs.Spark2.Specification.Helpers;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2
{
	public class DefaultSpecification : ISpecificationProvider
	{
		public IElementTransformerSpecification CreateSpecification()
		{
			var builder = new ElementTransformerSpecificationBuilder();

			builder.MatchAll(Tags.AnchorTag)
				.Do(
				Convert.ToAttributeToHref(),
				Convert.ToTypeAttributeToHref()
				);
			builder.MatchAll(Tags.IframeTag)
				.Do(
				Convert.ToAttributeToSrc(),
				Convert.ToTypeAttributeToSrc()
				);
			builder.MatchAll(Tags.ImageTag)
				.Do(
				Convert.ToAttributeToSrc(),
				Convert.ToTypeAttributeToSrc()
				);
			builder.MatchAll(Tags.FormTag)
				.Do(
				Convert.ForAttributeToAction(),
				Convert.ForTypeAttributeToAction()
				);
			builder.MatchAll(Tags.TextArea)
				.Do(
					Convert.ResourceValueToInnerText()
				);
			builder.MatchAll(Tags.InputTags)
				.Do(
				Convert.ForAttributeToName()
				);
			return builder.Build();
		}
	}
}