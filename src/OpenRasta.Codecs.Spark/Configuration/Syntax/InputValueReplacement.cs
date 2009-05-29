using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class InputValueReplacement : SpecifiedReplacement
	{
		public InputValueReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
		{
		}

		#region IReplacement Members

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			AttributeNode forAttribute = node.GetAttribute(ReplacementSpecification.OriginalAttributeName);
			AddAttribute(node, "value", forAttribute.Value.GetPropertyValueNode());
		}

		#endregion
	}
}