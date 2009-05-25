using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using OpenRasta.Web;

namespace OpenRasta.Codecs.Spark.Tests
{
	internal class TestUriResolver : Dictionary<string,Type>, IUriResolver
	{
		public const string TestEntityFormatString = "/TestEntity/{0}";
		public const string TestEntitiesUriString = "/TestEntities";

		public void AddUriMapping(string uri, Type resourceType, CultureInfo uriCulture, string name)
		{
			throw new System.NotImplementedException();
		}

		public ResourceMatch Match(Uri uriToMatch)
		{
			throw new System.NotImplementedException();
		}

		public void MakeReadOnly(Uri baseAddress)
		{
			throw new System.NotImplementedException();
		}

		public void Clear()
		{
			throw new System.NotImplementedException();
		}

		public string[] GetQueryParameterNamesFor(string uriTemplate)
		{
			throw new System.NotImplementedException();
		}

		public string[] GetTemplateParameterNamesFor(string uriTemplate)
		{
			throw new System.NotImplementedException();
		}

		public Uri CreateUriFor(Uri baseAddress, Type type, string uriName, NameValueCollection keyValues)
		{
			if(typeof(IEnumerable<TestEntity>).IsAssignableFrom(type))
			{
				return new Uri(baseAddress, TestEntitiesUriString);
			}
			if(typeof(TestEntity).IsAssignableFrom(type))
			{
				string name = "";
				if (keyValues != null)
				{
					name = keyValues["name"];
				}
				return new Uri(baseAddress, string.Format(TestEntityFormatString, name));
			}
			throw new InvalidOperationException("Dont recognise the type");
		}

		public Type this[string uriTemplate]
		{
			get { throw new System.NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}