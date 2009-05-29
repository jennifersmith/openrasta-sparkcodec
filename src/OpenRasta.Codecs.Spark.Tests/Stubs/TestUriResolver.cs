using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using OpenRasta.Codecs.Spark.Tests.TestObjects;
using OpenRasta.Web;

namespace OpenRasta.Codecs.Spark.Tests.Stubs
{
	internal class TestUriResolver : Dictionary<string, Type>, IUriResolver
	{
		public const string TestEntitiesUriString = "/TestEntities";
		public const string TestEntityFormatString = "/TestEntity/{0}";

		#region IUriResolver Members

		public void AddUriMapping(string uri, Type resourceType, CultureInfo uriCulture, string name)
		{
			throw new NotImplementedException();
		}

		public ResourceMatch Match(Uri uriToMatch)
		{
			throw new NotImplementedException();
		}

		public void MakeReadOnly(Uri baseAddress)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public string[] GetQueryParameterNamesFor(string uriTemplate)
		{
			throw new NotImplementedException();
		}

		public string[] GetTemplateParameterNamesFor(string uriTemplate)
		{
			throw new NotImplementedException();
		}

		public Uri CreateUriFor(Uri baseAddress, Type type, string uriName, NameValueCollection keyValues)
		{
			if (typeof (IEnumerable<TestEntity>).IsAssignableFrom(type))
			{
				return new Uri(baseAddress, TestEntitiesUriString);
			}
			if (typeof (TestEntity).IsAssignableFrom(type))
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
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}