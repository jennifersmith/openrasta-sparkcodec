using System;
using System.Linq.Expressions;
using OpenRasta.Reflection;

namespace OpenRasta.Codecs.Spark2.ViewHelpers
{
	public static class	PropertyPaths
	{
		public static string PathFor<T>(Expression<Func<T>> propertyAccessor)
		{
			return new PropertyPathForInstance<T>(propertyAccessor).FullPath;
		}
	}
}