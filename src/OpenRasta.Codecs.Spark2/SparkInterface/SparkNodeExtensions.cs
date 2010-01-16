using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public static class SparkNodeExtensions
	{
		public static IEnumerable<INode> WrapAll(this IEnumerable<Node> nodesToWrap)
		{
			return nodesToWrap.Select(x => SparkWrapperFactory.CreateWrapper(x));
		}
		public static IEnumerable<IElement> WrapAll(this IEnumerable<ElementNode> nodesToWrap)
		{
			return nodesToWrap.Select(x => SparkWrapperFactory.CreateWrapper(x)).Cast<IElement>();
		}
		public static Node Unwrap(this INode node)
		{
			return ((ISparkNodeWrapper) node).GetWrappedNode();
		}
		public static bool HasName(this ElementNode node, string name)
		{
			return node.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}