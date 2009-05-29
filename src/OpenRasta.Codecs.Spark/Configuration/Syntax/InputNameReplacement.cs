using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class InputNameReplacement : SpecifiedReplacement, IReplacement
	{
		public InputNameReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
		{
		}

		#region IReplacement Members

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			AttributeNode forAttribute = node.GetAttribute(ReplacementSpecification.OriginalAttributeName);
			AddAttribute(node, "name", new ExpressionNode(forAttribute.Value.GetPropertyNameSnippet()));
		}

		#endregion
	}
}