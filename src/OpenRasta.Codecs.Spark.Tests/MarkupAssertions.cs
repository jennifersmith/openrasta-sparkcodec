using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using NUnit.Framework;
using PanelSystem.WorkingDays.Tests;

namespace OpenRasta.Codecs.Spark.Tests
{
	/// <summary>
	/// using linq to xml to 
	/// </summary>
	static class MarkupAssertions
	{
		public static XElement HasElement(this string item, string name)
		{
			return item.HasElement(x=>string.Equals(x.Name.LocalName, name, StringComparison.CurrentCultureIgnoreCase));
		}
		public static  XElement HasElement(this string item, Func<XElement, bool> predicate)
		{
			var result = item.AsXml().Elements().Where(predicate).FirstOrDefault();
			Assert.That(result, Is.Not.Null, string.Format("Cannot find element matching {0} in xml {1}", predicate, item));
			return result;
		}

		public static XAttribute WithAttribute(this XElement element, string name)
		{
			// namespaces schamespaces
			var result = element.Attributes().Where(x => string.Equals(name, x.Name.LocalName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
			Assert.That(result, Is.Not.Null, string.Format("Cannot find attribute matching name {0} in element {1}", name, element));
			return result;
		}

		public static void WithoutAttribute(this XElement element, string name)
		{
			// namespaces schamespaces
			var result = element.Attributes().Where(x => string.Equals(name, x.Name.LocalName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
			Assert.That(result, Is.Null, string.Format("Element {1} should not have attribute matching name {0}", name, element));
		}
		public static void ShouldHaveValue(this XAttribute attribute, string value)
		{
			attribute.Value.ShouldEqual(value);
		}
		public static XElement AsXml(this string item)
		{
			XDocument doc = XDocument.Load(new StringReader("<documentElement>"+item+"</documentElement>"));
			return doc.Element(XName.Get("documentElement"));
		}
	}
}
