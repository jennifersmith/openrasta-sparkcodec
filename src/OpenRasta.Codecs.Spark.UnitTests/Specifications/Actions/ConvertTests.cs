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

		public string CreateNullCheckAndEvalExpression(string targetResource)
		{
			return GetTestNulLCheckAndEvalFor(targetResource);
		}

		public string CreateGetPropertyPathExpression(string propertyPath)
		{
			return GetTestGetPropertyPathExpression(propertyPath);
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
		public static string GetTestNulLCheckAndEvalFor(string targetResource)
		{
			return string.Format("GetTestNulLCheckAndEvalFor FOR {0}", targetResource);
		}

		public static string GetTestGetPropertyPathExpression(string propertyPath)
		{
			return string.Format("GETPROPERTYPATH FOR {0}", propertyPath);
		}
	}
	public static class StubSyntaxProviderExtensions
	{
		public static void ShouldBeGetPropertyPathExpressionFor(this TestAttributeNode attribute, string originalValue)
		{
			CodeExpression expectedPropertyPathExpresion = new CodeExpression(StubSyntaxProvider.GetTestGetPropertyPathExpression(originalValue));
			attribute.CodeNodes.First().As<TestCodeExpressionNode>().CodeExpression.ShouldEqual(expectedPropertyPathExpresion);
		}
		public static void ShouldBeCreateUriExpressionFor(this TestAttributeNode attribute, string originalValue)
		{
			attribute.ConditionalExpressionNodes.ShouldHaveCount(1);
			string createUriExpression = StubSyntaxProvider.GetTestCreateUriExpression(originalValue);
			string nullCheckExpression = StubSyntaxProvider.GetTestNullCheckExpression(originalValue);
			ConditionalExpression expectedExpression = new ConditionalExpression(nullCheckExpression, createUriExpression);
			attribute.ConditionalExpressionNodes.First().As<TestConditionalExpressionNode>().ConditionalExpression.ShouldEqual(expectedExpression);
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