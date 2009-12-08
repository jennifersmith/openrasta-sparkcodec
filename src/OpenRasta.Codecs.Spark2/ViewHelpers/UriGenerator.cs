using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OpenRasta.Web;

namespace OpenRasta.Codecs.Spark2.ViewHelpers
{
	public class UriGenerator : IUriGenerator
	{
		private IUriResolver resolver;
		public UriGenerator(IUriResolver resolver)
		{
			this.resolver = resolver;
		}

		public Uri Create(object input)
		{
			return input.CreateUri();
		}

		public Uri Create(Uri input)
		{
			return input;
		}
		public Uri Create<T>()
		{
			return resolver.CreateUriFor<T>();
		}
	}
}