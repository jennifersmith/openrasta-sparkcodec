using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class NullSparkElementTransformer : ISparkElementTransformer
	{
		public void Transform(IElement element)
		{
			throw new NotImplementedException();
		}
	}
}