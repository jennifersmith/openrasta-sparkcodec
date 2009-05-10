using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark
{
	public static class NodeExtensions
	{
		public	static bool IsTag(this ElementNode node, string tagName)
		{
			string nodeName = node.Name;
			return IsMatch(tagName, nodeName);
		}

		private static bool IsMatch(string x, string y)
		{
			return String.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
		}
		public static bool MatchesNames(this AttributeNode attribute, string[] name)
		{
			return name.Where(x => IsMatch(x, attribute.Name)).Any();
		}

		public static bool HasAttribute(this ElementNode node, string name)
		{
			return node.WhereAttributeNameIs(name).Any();
		}

		private static IEnumerable<AttributeNode> WhereAttributeNameIs(this ElementNode node, string name)
		{
			return node.Attributes.Where(x => IsMatch(name, x.Name));
		}

		public static string GetAttributeValue(this ElementNode node, string name)
		{
			var attribute = node.WhereAttributeNameIs(name).FirstOrDefault();
			if(attribute!=null)
			{
				return attribute.Value;
			}
			return "";
		}

		public static string GetAttributesAsFluentString(this ElementNode node, params string[] ignore)
		{
			// converting to fluent method calls which will be generated into html again. pointless?!
			StringBuilder builder = new StringBuilder();
			foreach (var attribute in node.Attributes)
			{
				if(attribute.MatchesNames(ignore))
				{
					continue;
				}
				builder.AppendFormat(".{0}(\"{1}\")", attribute.Name.Capitalize(), attribute.Value);
			}
			return builder.ToString();
		}
	}


	public static class StringExtensions
	{
		public static string Capitalize(this string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if(value.Length==1)
			{
				return value.ToUpper();
			}
			return string.Concat(char.ToUpper(value[0]), value.Substring(1).ToLower());
		}
	}
}