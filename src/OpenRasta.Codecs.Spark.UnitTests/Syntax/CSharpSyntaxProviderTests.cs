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
			ThenTheResultShouldBe("Uris.Create(myUriExpression)");
		}
		[Test]
		public void CreateUriFromTypeExpressionShouldReturnParameterWrappedInCorrectCodecall()
		{
			GivenAParam("ThisIsAType");
			WhenCreateUriFromTypeIsCalled();
			ThenTheResultShouldBe("Uris.Create<ThisIsAType>()");
		}
		[Test]
		public void NullCheckExpressionShouldReturnParameterWrappedInCorrectCodecall()
		{
			GivenAParam("myUriExpression");
			WhenNullCheckIscalled();
			ThenTheResultShouldBe("myUriExpression.IsNull()==false");
		}

		private void WhenNullCheckIscalled()
		{
			Context.Result = Context.Target.CreateNullCheckExpression(Context.InputParam);
		}

		private void ThenTheResultShouldBe(string expected)
		{
			Context.Result.ShouldEqual(expected);
		}

		private void WhenCreateUriIsCalled()
		{
			Context.Result = Context.Target.CreateUriExpression(Context.InputParam);
		}
		private void WhenCreateUriFromTypeIsCalled()
		{
			Context.Result = Context.Target.CreateUriFromTypeExpression(Context.InputParam);
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

			public string Result { get; set; }
		}
	}
}
