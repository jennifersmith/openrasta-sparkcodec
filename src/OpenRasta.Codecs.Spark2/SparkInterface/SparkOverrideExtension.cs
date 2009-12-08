using System.Collections.Generic;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkOverrideExtension : ISparkExtension
	{
		private readonly ElementNode _element;
		private readonly ISparkElementTransformer _sparkElementTransformer;

		public SparkOverrideExtension(ElementNode element, ISparkElementTransformer transformerService)
		{
			_element = element;
			_sparkElementTransformer = transformerService;
		}


		public ISparkElementTransformer SparkElementTransformer
		{
			get { return _sparkElementTransformer; }
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			SparkElementWrapper sparkElementWrapper = new SparkElementWrapper(_element, body);
			_sparkElementTransformer.Transform(sparkElementWrapper);
			visitor.Accept(_element);
			if (sparkElementWrapper.Body.Count > 0)
			{
				_element.IsEmptyElement = false;
				visitor.Accept(sparkElementWrapper.Body);
				visitor.Accept(new EndElementNode(_element.Name));
			}
			else
			{
				_element.IsEmptyElement = true;
			}
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			visitor.Accept(body);
		}
	}


	public interface ISparkNodeWrapper
	{
		Node GetWrappedNode();
	}

	public class UnrecognisedSparkNodeWrapper : SparkNodeWrapper<Node>
	{
		public UnrecognisedSparkNodeWrapper(Node node)
			: base(node)
		{
		}
	}
}