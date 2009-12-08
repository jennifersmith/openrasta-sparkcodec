using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public interface ISparkElementTransformer
	{
		void Transform(IElement element);
	}
}
