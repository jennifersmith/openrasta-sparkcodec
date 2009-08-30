using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace OpenRasta.Codecs.Spark.Testing.Extensions
{
	/// <summary>
	/// using linq to xml to 
	/// </summary>
	public static class MarkupAssertions
	{
		public static XElement HasElement(this string item, string name)
		{
			return item.HasElement(x => string.Equals(x.Name.LocalName, name, StringComparison.CurrentCultureIgnoreCase));
		}

		public static XElement HasElement(this XElement item, string name)
		{
			return item.HasElement(x => string.Equals(x.Name.LocalName, name, StringComparison.CurrentCultureIgnoreCase));
		}

		public static XElement HasElement(this string item, Func<XElement, bool> predicate)
		{
			XElement xElement = item.AsXml();
			return HasElement(xElement, predicate);
		}

		public static bool HasAttributeValue(this XElement element, string attributeName, string attributeValue)
		{
			return element.Attributes().Where(x => x.Name.LocalName.ToUpper() == attributeName.ToUpper()).Where(
				x => x.Value == attributeValue).Any();
		}

		public static bool HasNoAttributeValue(this XElement element, string attributeName, string attributeValue)
		{
			return element.HasAttributeValue(attributeName, attributeValue) == false;
		}

		public static XElement HasElement(this XContainer xElement, Func<XElement, bool> predicate)
		{
			XElement result = xElement.Elements().Where(predicate).FirstOrDefault();
			Assert.That(result, Is.Not.Null, string.Format("Cannot find element matching {0} in xml {1}", predicate, xElement));
			return result;
		}

		public static XAttribute WithAttribute(this XElement element, string name)
		{
			// namespaces schamespaces
			XAttribute result =
				element.Attributes().Where(x => string.Equals(name, x.Name.LocalName, StringComparison.CurrentCultureIgnoreCase)).
					FirstOrDefault();
			Assert.That(result, Is.Not.Null,
			            string.Format("Cannot find attribute matching name {0} in element {1}", name, element));
			return result;
		}

		public static void WithoutAttribute(this XElement element, string name)
		{
			// namespaces schamespaces
			XAttribute result =
				element.Attributes().Where(x => string.Equals(name, x.Name.LocalName, StringComparison.CurrentCultureIgnoreCase)).
					FirstOrDefault();
			Assert.That(result, Is.Null, string.Format("Element {1} should not have attribute matching name {0}", name, element));
		}

		public static void ShouldHaveValue(this XAttribute attribute, string value)
		{
			attribute.Value.ShouldEqual(value);
		}

		public static XElement AsXml(this string item)
		{
			XDocument doc = XDocument.Load(new StringReader("<documentElement>" + item + "</documentElement>"));
			return doc.Element(XName.Get("documentElement"));
		}
	}
}