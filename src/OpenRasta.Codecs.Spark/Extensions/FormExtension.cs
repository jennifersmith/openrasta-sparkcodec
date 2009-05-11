using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Web.Markup;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class FormExtension : ISparkExtension
	{
		private readonly ElementNode node;

		public FormExtension(ElementNode node)
		{
			this.node = node;
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			visitor.Accept(body);
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			
			if (location == OutputLocation.RenderMethod)
			{
				//todo: rf
				string forType = node.GetAttributeValue("forType");
				string forValue = node.GetAttributeValue("for");
				string fluentMethods = node.GetAttributesAsFluentString("forType", "for");
				if (!string.IsNullOrEmpty(forType))
				{
					output.AppendLine(string.Format("using (scope(Xhtml.Form<{0}>(){1})){{", forType, fluentMethods));
					visitor.Accept(body);
					output.AppendLine("}");
				}
				else
				{
					output.AppendLine(string.Format("using (scope(Xhtml.Form({0}){1})){{", forValue, fluentMethods));
					visitor.Accept(body);
					output.AppendLine("}");

				}
			}
		}
	}
}