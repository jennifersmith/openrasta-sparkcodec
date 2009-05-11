using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using OpenRasta.Web;

namespace OpenRasta.Codecs.Spark.Tests
{
	internal class TestUriResolver : Dictionary<string,Type>, IUriResolver
	{
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
				return new Uri(baseAddress, "/TestEntities");
			}
			if(typeof(TestEntity).IsAssignableFrom(type))
			{
				string name = "";
				if (keyValues != null)
				{
					name = keyValues["name"];
				}
				return new Uri(baseAddress, "/TestEntity/" + name);
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