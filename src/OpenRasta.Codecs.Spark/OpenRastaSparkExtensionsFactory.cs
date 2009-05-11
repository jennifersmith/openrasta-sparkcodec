using OpenRasta.Codecs.Spark.Extensions;
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
			if (IsResourceAnchorTag(node))
			{
				return new AnchorTagExtension(node);
			}
			if (IsResourceFormTag(node))
			{
				return new FormExtension(node);
			}
			if (IsInputTag(node)||IsTextareaTag(node))
			{
				return new InputExtensions(node);
			}
			return null;
		}

		#endregion

		private static bool IsResourceAnchorTag(ElementNode node)
		{
			return node.IsTag("a") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}

		private static bool IsResourceFormTag(ElementNode node)
		{
			return node.IsTag("form") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}

		private static bool IsInputTag(ElementNode node)
		{
			return node.IsTag("input") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}

		private static bool IsTextareaTag(ElementNode node)
		{
			return node.IsTag("textarea") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}
	}
}