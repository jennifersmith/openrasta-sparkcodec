using System.Collections.Generic;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	internal class SparkExtension : ISparkExtension
	{
		private readonly IEnumerable<IReplacement> replacements;

		public SparkExtension(ElementNode node, IEnumerable<IReplacement> replacements)
		{
			this.replacements = replacements;
			Node = node;
		}

		public ElementNode Node { get; private set; }

		#region ISparkExtension Members

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			Transform(Node, body);

			visitor.Accept(Node);
			visitor.Accept(body);
			if (!Node.IsEmptyElement)
			{
				visitor.Accept(new EndElementNode(Node.Name));
			}
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			visitor.Accept(body);
		}

		#endregion

		protected  void Transform(ElementNode elementNode, IList<Node> body)
		{
			foreach (IReplacement replacement in replacements)
			{
				replacement.DoReplace(elementNode);
			}
		}
	}

}