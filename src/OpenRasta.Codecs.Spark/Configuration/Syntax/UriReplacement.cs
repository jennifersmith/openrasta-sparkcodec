using System.Collections.Generic;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Configuration.Syntax
{
	internal class UriReplacement : SpecifiedReplacement
	{
		public UriReplacement(ReplacementSpecification replacementSpecification) : base(replacementSpecification)
		{
		}

		public override void DoReplace(ElementNode node, IList<Node> body)
		{
			RemoveReplacedAttribute(node);
			Node uriSnippet = RemoteAndEvalUriSnippet(node);

			node.Attributes.Add(new AttributeNode(ReplacementSpecification.NewAttributeName, new List<Node>
			                                                                                 	{
			                                                                                 		uriSnippet
			                                                                                 	}));
		}

		private Node RemoteAndEvalUriSnippet(ElementNode elementNode)
		{
			string attribValue = elementNode.GetAttributeValue(ReplacementSpecification.OriginalAttributeName);
			elementNode.RemoveAttributesByName(ReplacementSpecification.OriginalAttributeName);
			return
				attribValue.GetCreateUriSnippet(
					IsTypeReplacement());
		}

		private bool IsTypeReplacement()
		{
			return ReplacementSpecification.OriginalAttributeName.ToUpper() == "TOTYPE";
		}

		private void RemoveReplacedAttribute(ElementNode elementNode)
		{
			elementNode.RemoveAttributesByName(ReplacementSpecification.NewAttributeName);
		}
	}
}