using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using OpenRasta.Reflection;
using OpenRasta.Web;
using Spark.Parser.Code;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	public static class CodeGenerationExtensions
	{
		public static Node GetCreateUriSnippet(this string entity, bool isType)
		{
			if (isType)
			{
				entity = "typeof(" + entity + ")";
				return new ExpressionNode(entity + ".CreateUri()");
			}
			ConditionNode nullCheck = new ConditionNode(entity.GetIsNotNullExpression());
			nullCheck.Nodes.Add(new ExpressionNode(entity + ".CreateUri()"));
			return nullCheck;
		}
		public static string GetIsNotNullExpression(this string entity)
		{
			return string.Format("{0}!=null", entity);
		}
		public static string GetPropertyNameSnippet(this string propertyAccessor)
		{
			// hardcoding pretty dodgy
			const string format = "CodeGenerationExtensions.GetPropertyName(()=>{0})";
			return string.Format(format, propertyAccessor);
		}

		public static string GetPropertyName(Expression<Func<object>> expression)
		{
			return new PropertyPathExpressionTree(expression).Path;
		}
	}
}
