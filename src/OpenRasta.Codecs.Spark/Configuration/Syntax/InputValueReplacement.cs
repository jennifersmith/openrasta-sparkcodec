using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Configuration.Syntax
{
	internal class InputValueReplacement : SpecifiedReplacement
	{
		public InputValueReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
		{
		}

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			AttributeNode forAttribute = node.GetAttribute(ReplacementSpecification.OriginalAttributeName);
			AddAttribute(node, "value", forAttribute.Value.GetPropertyValueNode());
		}
	}
}