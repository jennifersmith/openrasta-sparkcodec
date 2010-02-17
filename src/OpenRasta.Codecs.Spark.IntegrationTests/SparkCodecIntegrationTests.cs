using System;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark.Configuration;
using OpenRasta.Codecs.Spark.Testing.Extensions;
using OpenRasta.Configuration;
using OpenRasta.Configuration.Fluent;
using OpenRasta.Hosting;
using OpenRasta.Tests.Integration;
using OpenRasta.Web;
using OpenRasta.Web.UriDecorators;

namespace OpenRasta.Codecs.Spark.IntegrationTests
{
	[TestFixture]
	public class SparkCodecIntegrationTests : server_context
	{
		public SparkCodecIntegrationTests()
		{

			ConfigureServer(() =>
			{
				ResourceSpace.Has.ResourcesOfType<Customer>()
					.AtUri("/Test123")
					.HandledBy<SparkTestHandler>().AndRenderedBySpark("BlaH");
					;

					ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
				ResourceSpace.Uses.SparkCodec();
			});
		}
		[Test]
		[Ignore("Arrgh I need to get this working...")]

		public void GetShouldReturnRenderedTemplate()
		{
			GivenATemplate("Test123", "<p>Hello there</p>");
			given_request("GET", "/Test123.html", new byte[0], MediaType.Html);
			when_reading_response_as_a_string(Encoding.Default);
			ThenResultShouldBe("<p>Hello there</p>");
		}

		private void ThenResultShouldBe(string expected)
		{
			TheResponseAsString.ShouldEqual(expected);
		}

		private void WhenGetting(string uri)
		{
			given_request("get", uri);
		}

		private void GivenATemplate(string url, string template)
		{
		}
	}
	public class Customer{}
	public class SparkTestHandler 
	{
		public OperationResult Get()
		{
			return new OperationResult.OK(new { Test = "foo", Bar = "test" });
		}
	}
}