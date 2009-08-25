using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark.Configuration.Syntax;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class SelectSelectedValueReplacement : SpecifiedReplacement
	{
		public SelectSelectedValueReplacement(ReplacementSpecification replacementSpecification)
			: base(replacementSpecification)
		{
		}

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			IEnumerable<ElementNode> options = GetOptions(body);
			foreach (ElementNode option in options)
			{
				string currentSelectedValue = option.GetAttributeValue("selected");
				string propertyValue = option.GetAttributeValue("value");
				string propertyExpression = node.GetAttributeValue(ReplacementSpecification.OriginalAttributeName);
				Node selectedNode = propertyExpression.GetSelectedSnippet(currentSelectedValue, propertyValue);
				option.RemoveAttributesByName("selected");
				AddAttribute(option, "selected", selectedNode);
			}
		}

		private IEnumerable<ElementNode> GetOptions(IList<Node> body)
		{
			return body.Where(x => x is ElementNode).Cast<ElementNode>().Where(x => x.IsTag("option"));
		}
	}
}