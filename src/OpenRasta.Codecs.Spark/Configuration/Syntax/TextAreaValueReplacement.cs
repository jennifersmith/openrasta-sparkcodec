using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class TextAreaValueReplacement : SpecifiedReplacement
	{
		public TextAreaValueReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
		{
		}

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			body.Clear();
			Node newBody = node.GetAttribute("for").Value.GetPropertyValueNode();
			body.Add(newBody);
		}
	}
}