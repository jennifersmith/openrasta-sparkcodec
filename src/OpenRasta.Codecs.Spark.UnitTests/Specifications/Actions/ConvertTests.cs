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

		public string CreateUriFromTypeExpression(string targetResource)
		{
			return GetTestCreateUriFromTypeExpression(targetResource);
		}

		public string CreateNullCheckExpression(string targetResource)
		{
			return GetTestNullCheckExpression(targetResource);
		}

		public static string GetTestCreateUriExpression(string targetResource)
		{
			return string.Format("CREATEURI FOR {0}", targetResource);
		}
		public static string GetTestCreateUriFromTypeExpression(string targetResource)
		{
			return string.Format("CREATEURIFROMTYPE FOR {0}", targetResource);
		}
		public static string GetTestNullCheckExpression(string targetResource)
		{

			return string.Format("NULLCHECK FOR {0}", targetResource);
		}
	}
	public static class StubSyntaxProviderExtensions
	{
		public static void ShouldBeCreateUriExpressionFor(this TestAttributeNode attribute, string originalValue)
		{
			attribute.CodeExpressionNodes.ShouldHaveCount(1);
			string createUriExpression = StubSyntaxProvider.GetTestCreateUriExpression(originalValue);
			string nullCheckExpression = StubSyntaxProvider.GetTestNullCheckExpression(originalValue);
			ConditionalExpression expectedExpression = new ConditionalExpression(nullCheckExpression, createUriExpression);
			attribute.CodeExpressionNodes.First().As<TestConditionalExpressionNode>().ConditionalExpression.ShouldEqual(expectedExpression);
		}
		public static void ShouldBeCreateUriFromTypeExpressionFor(this TestAttributeNode attribute, string originalValue)
		{
			attribute.CodeNodes.ShouldHaveCount(1);
			string createUriExpression = StubSyntaxProvider.GetTestCreateUriFromTypeExpression(originalValue);
			CodeExpression expectedExpression = new CodeExpression(createUriExpression);
			attribute.CodeNodes.First().As<TestCodeExpressionNode>().CodeExpression.ShouldEqual(expectedExpression);
		}
	}
}