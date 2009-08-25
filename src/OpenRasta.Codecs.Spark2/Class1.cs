using System;
using System.Collections.Generic;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class CodecSparkExtensionFactory : ISparkExtensionFactory
	{
		private readonly IElementTransformerService _elementTransformerService;

		public CodecSparkExtensionFactory(IElementTransformerService elementTransformerService)
		{
			_elementTransformerService = elementTransformerService;
		}

		public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
		{
			if (_elementTransformerService.ElementIsOverridable(node))
			{
				return new SparkOverrideExtension(_elementTransformerService.CreateElementTransformer(node));
			}
			return null;
		}
	}

	public class SparkOverrideExtension : ISparkExtension
	{
		private readonly IElementTransformer _elementTransformer;

		public SparkOverrideExtension(IElementTransformer transformerService)
		{
			_elementTransformer = transformerService;
		}


		public IElementTransformer ElementTransformer
		{
			get { return _elementTransformer; }
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			Node transformed = _elementTransformer.Transform(body);
			visitor.Accept(transformed);
			if(IsNonEmptyElement(transformed))
			{
				visitor.Accept(body);
				visitor.Accept(new EndElementNode(((ElementNode)transformed).Name));
			}
		}

		private static bool IsNonEmptyElement(Node node)
		{
			return node is ElementNode && !((ElementNode) node).IsEmptyElement;
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
		}
	}

	public interface IElementTransformerService
	{
		bool ElementIsOverridable(ElementNode node);
		IElementTransformer CreateElementTransformer(ElementNode elementNode);
	}

	public interface IElementTransformer
	{
		Node Transform(IList<Node> innerNodes);
	}
}
