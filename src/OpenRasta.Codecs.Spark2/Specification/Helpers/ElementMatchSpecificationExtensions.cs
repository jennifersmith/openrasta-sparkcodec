using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Specification.Builders;

namespace OpenRasta.Codecs.Spark2.Specification.Syntax
{
	public static class ElementMatchSpecificationExtensions
	{
		public static IElementTransformerActionsByMatchBuilder MatchAll(this ElementTransformerSpecificationBuilder config, params Tag[] tags)
		{
			return MatchAll(config, (IEnumerable<Tag>) tags);
		}
		public static IElementTransformerActionsByMatchBuilder MatchAll(this ElementTransformerSpecificationBuilder config, IEnumerable<Tag> tags)
		{
			var builder = new ElementTransformerActionsByMatchBuilder(tags);
			config.AddTranformationBuilder(builder);
			return builder;
		}
	}
}