using OpenRasta.Web;
using OpenRasta.Web.Markup;
using Spark;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark
{
	internal class OpenRastaSparkExtensionsFactory : ISparkExtensionFactory
	{
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
			if (IsResourceTextBoxTag(node))
			{
				return new TextBoxExtension(node);
			}
			return null;
		}

		private static bool IsResourceAnchorTag(ElementNode node)
		{
			return node.IsTag("a") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}
		private static bool IsResourceFormTag(ElementNode node)
		{
			return node.IsTag("form") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}
		private static bool IsResourceTextBoxTag(ElementNode node)
		{
			return node.IsTag("textbox") && (node.HasAttribute("for") || node.HasAttribute("fortype"));
		}
	}
}