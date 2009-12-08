using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRasta.Codecs.Spark2
{
	public static class MissingEnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}
		public static bool Contains<T>(this IEnumerable<T> enumerable, Func<T, bool> action)
		{
			return enumerable.Where(action).Any();
		}
	}
}