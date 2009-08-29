using System;
using System.Collections.Generic;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class NullSparkElementTransformer : ISparkElementTransformer
	{
		public Node Transform(IList<Node> innerNodes)
		{
			throw new NotImplementedException();
		}
	}
}