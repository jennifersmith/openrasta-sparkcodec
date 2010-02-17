using System;
using Spark;

namespace OpenRasta.Codecs.Spark.IntegrationTests
{
	public abstract class TestSparkView : AbstractSparkView
	{
		public TestViewData ViewData { set; get; }
	}

	public class TestViewData
	{
		private readonly object resource;

		public TestViewData(object resource)
		{
			this.resource = resource;
		}


		public object Eval(string expression)
		{
			if (Matches(expression, "Resource"))
			{
				return resource;
			}
			return null;
		}

		private static bool Matches(string expression, string name)
		{
			return string.Equals(expression, name, StringComparison.CurrentCultureIgnoreCase);
		}

		public string Eval(string expression, string format)
		{
			// not sure if this will be used;
			return string.Format(format, Eval(expression));
		}
	}

}