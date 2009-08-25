using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark.Configuration.Syntax;
using OpenRasta.Codecs.Spark.Configuration.Syntax.Specifications;
using Spark;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark
{
	internal class OpenRastaSparkExtensionsFactory : ISparkExtensionFactory
	{
		#region ISparkExtensionFactory Members

		public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
		{
			IEnumerable<IReplacement> replacements = GetApplicableReplacements(node);
			if (replacements.Any())
			{
				return new SparkExtension(node, replacements);
			}
			return null;
		}

		#endregion

		private static IEnumerable<IReplacement> GetApplicableReplacements(ElementNode node)
		{
			var result = new List<IReplacement>();
			// rf this crap
			if ((node.Name == "input") && (node.HasAttribute("type") && (node.HasAttribute("for"))))
			{
				if (node.Attributes.Where(x => (x.Name == "type") && (x.Value == "checkbox")).Any())
				{
					result.Add(new CheckBoxCheckedReplacement());
				}
			}
			result.AddRange(UriReplacementSpecifications.GetMatching(node));
			result.AddRange(FormReplacementSpecifications.GetMatching(node));
			
			return result;
		}
	}

	internal class CheckBoxCheckedReplacement : IReplacement
	{
		public void DoReplace(ElementNode node, IList<Node> body)
		{
			string propertyExpression = node.GetAttributeValue("for");
			Node checkedSnippet = propertyExpression.GetCheckedSnippet();
			node.RemoveAttributesByName("checked");
			node.Attributes.Add(new AttributeNode("checked", new List<Node>(){checkedSnippet}));
		}
	}
}