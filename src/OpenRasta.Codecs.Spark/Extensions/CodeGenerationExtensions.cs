using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using OpenRasta.Reflection;
using Spark.Parser.Code;

namespace OpenRasta.Codecs.Spark.Extensions
{
	public static class CodeGenerationExtensions
	{
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
