using System;
using System.Collections.Generic;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Code;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkOverrideExtension : ISparkExtension
	{
		private readonly ISparkElementTransformer _sparkElementTransformer;

		public SparkOverrideExtension(ISparkElementTransformer transformerService)
		{
			_sparkElementTransformer = transformerService;
		}


		public ISparkElementTransformer SparkElementTransformer
		{
			get { return _sparkElementTransformer; }
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			Node transformed = _sparkElementTransformer.Transform(body);
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


	public abstract class SparkNodeWrapper
	{
		public abstract Node GetWrappedNode();
	}

	public class SparkAttributeWrapper : SparkNodeWrapper<AttributeNode>, IAttribute
	{
		public SparkAttributeWrapper(AttributeNode node) : base(node)
		{
		}
		public void AddExpressionBody(string codeSnippet)
		{
			throw new NotImplementedException();
		}

		public ICodeExpressionNode AddExpression()
		{
			ExpressionNode node = new ExpressionNode(new Snippets());
			throw new NotImplementedException();
		}

		public string GetTextValue()
		{
			throw new NotImplementedException();
		}

		public bool Exists()
		{
			throw new NotImplementedException();
		}
	}
	public class UnrecognisedSparkNodeWrapper : SparkNodeWrapper<Node>
	{
		public UnrecognisedSparkNodeWrapper(Node node)
			: base(node)
		{
		}
	}
}