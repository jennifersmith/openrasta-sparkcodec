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

		public void AddUriMapping(string uri, object resourceKey, CultureInfo uriCulture, string uriName)
		{
			throw new NotImplementedException();
		}

		public ResourceMatch Match(Uri uriToMatch)
		{
			throw new NotImplementedException();
		}

		public Uri CreateUriFor(Uri baseAddress, object obj, string uriName, NameValueCollection keyValues)
		{
			var type = (Type) obj;
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

		#endregion

		public new IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}