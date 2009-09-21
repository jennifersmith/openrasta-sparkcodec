using OpenRasta.Codecs.Spark2.SparkInterface;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.UnitTests.SparkInterface
{
	public class TestSparkNodeWrapper : SparkNodeWrapper<ElementNode>
	{
		public TestSparkNodeWrapper(ElementNode node)
			: base(node)
		{
		}
	}
}