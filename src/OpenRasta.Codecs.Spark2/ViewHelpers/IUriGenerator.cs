using System;

namespace OpenRasta.Codecs.Spark2.ViewHelpers
{
	public interface IUriGenerator
	{
		Uri Create(object input);
		Uri Create(Uri input);
		Uri Create<T>();
	}
}