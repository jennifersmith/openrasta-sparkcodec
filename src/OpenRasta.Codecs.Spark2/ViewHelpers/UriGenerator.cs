using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Web;

namespace OpenRasta.Codecs.Spark2.ViewHelpers
{		
	public static class NullCheckExtensions
	{
		public static bool IsNull(this object target)
		{
			return target == null;
		}
	}
	public interface IUriGenerator
	{
		Uri Create(object input);
		Uri Create(Uri input);
	}

	public class UriGenerator : IUriGenerator
	{
		public UriGenerator()
		{
		}

		public Uri Create(object input)
		{
			return input.CreateUri();
		}

		public Uri Create(Uri input)
		{
			return input;
		}
	}
}