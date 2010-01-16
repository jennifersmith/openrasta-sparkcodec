using System;
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
				return new SparkElementWrapper((ElementNode)node, new Node[0]);
			}
			if (node is AttributeNode)
			{
				return new SparkAttributeWrapper((AttributeNode)node);
			}
			return new UnrecognisedSparkNodeWrapper(node);
		}
	}
}
