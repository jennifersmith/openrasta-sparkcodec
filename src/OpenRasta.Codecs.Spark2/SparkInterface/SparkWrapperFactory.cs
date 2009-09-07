using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public static class SparkWrapperFactory
	{
		public static INode CreateWrapper(Node node)
		{
			if (node is ElementNode)
			{
				return new SparkElementWrapper((ElementNode)node);
			}
			if (node is AttributeNode)
			{
				return new SparkAttributeWrapper((AttributeNode)node);
			}
			return new UnrecognisedSparkNodeWrapper(node);
		}
	}

	public static class SparkNodeExtensions
	{
		public static IEnumerable<INode> WrapAll(this IEnumerable<Node> nodesToWrap)
		{
			return nodesToWrap.Select(x => SparkWrapperFactory.CreateWrapper(x));
		}
		public static Node Unwrap(this INode node)
		{
			return ((SparkNodeWrapper) node).WrappedNode;
		}
	}

}
