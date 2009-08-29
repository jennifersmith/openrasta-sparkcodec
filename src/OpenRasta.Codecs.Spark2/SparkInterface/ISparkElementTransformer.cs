using System.Collections.Generic;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public interface ISparkElementTransformer
	{
		Node Transform(IList<Node> innerNodes);
	}
}
