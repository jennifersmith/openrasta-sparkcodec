using OpenRasta.Codecs.Spark2.Model;
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
				)
				.Finally(
					Remove.ToAttributes(), 
					Remove.ToTypeAttributes()
				);
			builder.MatchAll(Tags.IframeTag)
				.Do(
				Convert.ToAttributeToSrc(),
				Convert.ToTypeAttributeToSrc()
				)
				.Finally(
					Remove.ToAttributes(),
					Remove.ToTypeAttributes()
				); 
			builder.MatchAll(Tags.ImageTag)
				.Do(
				Convert.ToAttributeToSrc(),
				Convert.ToTypeAttributeToSrc()
				)
				.Finally(
					Remove.ToAttributes(),
					Remove.ToTypeAttributes()
				); 
			builder.MatchAll(Tags.FormTag)
				.Do(
				Convert.ForAttributeToAction(),
				Convert.ForTypeAttributeToAction()
				)
				.Finally(
					Remove.ForAttributes(),
					Remove.ForTypeAttributes()
				);
			builder.MatchAll(Tags.TextArea)
				.Do(
					Convert.ResourceValueToInnerText()
				)
				.Finally(
					Remove.ForAttributes()
				); 
			builder.MatchAll(Tags.CheckTag)
				.Do(
					Convert.ResourceValueToCheckedAttribute()
				)
				.Finally(
					Remove.ForAttributes()
				);
			builder.MatchAll(Tags.Select)
				.Do(
					Convert.ResourceValueToSelectedOption()
				)
				.Finally(
					Remove.ForAttributes()
				);
			builder.MatchAll(Tags.InputTags)
				.Do(
				Convert.ForAttributeToName()
				)
				.Finally(
					Remove.ForAttributes()
				);
			return builder.Build();
		}
	}
}