using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using OpenRasta.Reflection;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Extensions
{
	public static class CodeGenerationExtensions
	{
		private static readonly Regex objectNameRegex = new Regex("^(?<object>[a-zA-Z_][a-zA-Z0-9_]*)");

		public static Node GetCreateUriSnippet(this string entity, bool isType)
		{
			if (isType)
			{
				entity = "typeof(" + entity + ")";
				return new ExpressionNode(entity + ".CreateUri()");
			}
			var nullCheck = new ConditionNode(entity.GetIsNotNullExpression());
			nullCheck.Nodes.Add(new ExpressionNode(entity + ".CreateUri()"));
			return nullCheck;
		}

		public static string GetIsNotNullExpression(this string entity)
		{
			return string.Format("({0}!=null)", entity);
		}

		public static string GetPropertyNameSnippet(this string propertyAccessor)
		{
			// hardcoding pretty dodgy
			const string format = "CodeGenerationExtensions.GetPropertyName(()=>{0})";
			return string.Format(format, propertyAccessor);
		}

		public static Node GetPropertyValueNode(this string propertyAccessor)
		{
			string objectName = propertyAccessor.GetObjectName();
			var conditionNode = new ConditionNode(string.Format("{0}!=null", objectName));
			conditionNode.Nodes.Add(new ExpressionNode(propertyAccessor));
			return conditionNode;
		}

		public static Node GetSelectedSnippet(this string propertyAccessor, string currentSelectedState, string optionValue)
		{
			string propertyNullTest = propertyAccessor.GetObjectName().GetIsNotNullExpression();
			string currentSelectedValueTest = string.Format(@"(""{0}""!="""")", currentSelectedState);
			string propertyValueTest = string.Format("({0}==\"{1}\")", propertyAccessor, optionValue);
			// argh
			// if ((propertyNullTest)&&(propertyValueTest))||(currentSelectedValueTest))
			string conidtion = string.Format("({0}&&{1})||{2}",
			                                 propertyNullTest, propertyValueTest, currentSelectedValueTest);
			var node = new ConditionNode(conidtion);
			node.Nodes.Add(new TextNode("true"));
			return node;
		}

		public static string GetObjectName(this string propertyPath)
		{
			Match match = objectNameRegex.Match(propertyPath);
			if (match.Success)
			{
				return match.Groups["object"].Value;
			}
			return "";
		}

		public static string GetPropertyName(Expression<Func<object>> expression)
		{
			return new PropertyPathForInstance<object>(expression).FullPath;
		}
	}
}