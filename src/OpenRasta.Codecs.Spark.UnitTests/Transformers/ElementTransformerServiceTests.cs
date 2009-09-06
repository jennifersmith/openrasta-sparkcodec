using System;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Transformers;

namespace OpenRasta.Codecs.Spark.UnitTests.Transformers
{
	[TestFixture]
	public class ElementTransformerServiceTests
	{
		public void SetUp()
		{
			Context = new ElementTransformerServiceTestContext
			          	{
			          		ElementTransformsConfiguration = new StubElementTransformsConfiguration()
			          	};
			Context.Target = new ElementTransformerService(Context.ElementTransformsConfiguration);
		}

		protected ElementTransformerServiceTestContext Context { get; set; }

		public void ShouldReturnAnElementTransformerContainingAllMatchesForCurrentNode()
		{
			Assert.Fail("Need to work on node matching/config stuff");
		}

		public class ElementTransformerServiceTestContext
		{
			public StubElementTransformsConfiguration ElementTransformsConfiguration { get; set; }

			public ElementTransformerService Target { get; set; }
		}
	}


	public class StubElementTransformsConfiguration : IElementTransformsConfiguration
	{
	}
}
