using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark.UnitTests.Syntax
{
	[TestFixture]
	public class CSharpSyntaxProviderTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new CSharpSyntaxProviderTestContext();
			Context.Target = new CSharpSyntaxProvider();
		}
		[Test]
		public void CreateUriExpressionShouldReturnParameterWrappedInCorrectCodecall()
		{
			GivenAParam("myUriExpression");
			WhenCreateUriIsCalled();
			ThenTheResultShouldBe("UriCreator.Create(\'myUriExpression\')");
		}

		private void ThenTheResultShouldBe(string expected)
		{
			Context.CreateUriResult.ShouldEqual(expected);
		}

		private void WhenCreateUriIsCalled()
		{
			Context.CreateUriResult = Context.Target.CreateUriExpression(Context.InputParam);
		}

		private void GivenAParam(string myuriexpression)
		{
			Context.InputParam = myuriexpression;
		}


		public CSharpSyntaxProviderTestContext Context { get; set; }

		public class CSharpSyntaxProviderTestContext
		{
			public CSharpSyntaxProvider Target { get; set; }

			public string InputParam { get; set; }

			public string CreateUriResult { get; set; }
		}
	}
}
