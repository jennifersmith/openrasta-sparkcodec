using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class TidyUpReplacement : SpecifiedReplacement
	{
		public TidyUpReplacement(ReplacementSpecification replacementSpecification)
			: base(replacementSpecification)
		{
		}

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			node.RemoveAttributesByName(ReplacementSpecification.OriginalAttributeName);
		}
	}
}