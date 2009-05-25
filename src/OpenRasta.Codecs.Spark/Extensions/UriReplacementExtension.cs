using System.Collections.Generic;
using System.Text;
using OpenRasta.Codecs.Spark.Extensions.Specifications;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{

	internal interface IReplacement
	{
		void DoReplace(ElementNode node);
	}

	class UriReplacement : IReplacement
	{
		private readonly ReplacementSpecification replacementSpecification;

		public UriReplacement(ReplacementSpecification replacementSpecification)
		{
			this.replacementSpecification = replacementSpecification;
		}

		public void DoReplace(ElementNode node)
		{
			RemoveReplacedAttribute(node);
			Node uriSnippet = RemoteAndEvalUriSnippet(node);

			node.Attributes.Add(new AttributeNode(replacementSpecification.NewAttributeName, new List<Node>()
			                                                                                 	{
			                                                                                 		uriSnippet
			                                                                                 	}));
		}

		private Node RemoteAndEvalUriSnippet(ElementNode elementNode)
		{
			string attribValue = elementNode.GetAttributeValue(replacementSpecification.OriginalAttributeName);
			elementNode.RemoveAttributesByName(replacementSpecification.OriginalAttributeName);
			return
				attribValue.GetCreateUriSnippet(
					IsTypeReplacement());
		}

		private bool IsTypeReplacement()
		{
			return replacementSpecification.OriginalAttributeName.ToUpper() == "TOTYPE";
		}

		private void RemoveReplacedAttribute(ElementNode elementNode)
		{
			elementNode.RemoveAttributesByName(replacementSpecification.NewAttributeName);
		}
	}
}