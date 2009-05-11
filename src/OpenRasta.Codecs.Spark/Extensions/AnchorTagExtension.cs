using System.Collections.Generic;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class AnchorTagExtension : ISparkExtension
	{
		private readonly ElementNode node;

		public AnchorTagExtension(ElementNode node)
		{
			this.node = node;
		}

		#region ISparkExtension Members

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			visitor.Accept(body);
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			if (location == OutputLocation.RenderMethod)
			{
				string forType = node.GetAttributeValue("forType");
				string forResource = node.GetAttributeValue("for");
				string attributes = node.GetAttributesAsFluentString("forType", "for");
				string innerText = GetInnerText(body);
				if (!string.IsNullOrEmpty(forType))
				{
					output.AppendFormat("Output.Write(Xhtml.Link<{0}>()", forType);
				}
				else if (!string.IsNullOrEmpty(forResource))
				{
					output.AppendFormat("Output.Write(Xhtml.Link({0})", forResource);
				}
				output.Append(attributes);
				output.AppendFormat("[\"{0}\"]);\r\n", innerText);
			}
		}

		#endregion

		private string GetInnerText(IList<Chunk> chunks)
		{
			StringBuilder result = new StringBuilder();
			foreach (var chunk in chunks)
			{
				if (chunk is SendLiteralChunk)
				{
					result.Append(((SendLiteralChunk) chunk).Text);
				}
			}
			return result.ToString();
		}
	}
}