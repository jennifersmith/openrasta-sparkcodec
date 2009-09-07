using System;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Syntax;

namespace OpenRasta.Codecs.Spark.UnitTests.Specifications.Actions
{
	public class StubSyntaxProvider : ISyntaxProvider
	{
		public string CreateUriExpression(string targetResource)
		{
			return GetTestCreateUriExpression(targetResource);
		}

		public static string GetTestCreateUriExpression(string targetResource)
		{
			return string.Format("CREATEURI FOR {0}", targetResource);
		}
	}
	public static class StubSyntaxProviderExtensions
	{
		public static void ShouldBeCreateUriExpressionFor(this TestAttributeNode attribute, string originalValue)
		{
			attribute.CodeNodes.ShouldHaveCount(1);
			string createUriExpression = StubSyntaxProvider.GetTestCreateUriExpression(originalValue);
			attribute.CodeNodes.First().As<TestCodeExpressionNode>().Body.ShouldEqual(createUriExpression);
		}
	}
}