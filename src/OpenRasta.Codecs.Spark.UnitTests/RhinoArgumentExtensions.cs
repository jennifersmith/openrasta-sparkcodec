using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.Mocks;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public static class RhinoArgumentExtensions
	{
		public static object GetArgumentsForSingleCall<T>(this T target, Action<T> action)
		{
			IEnumerable<object> args = target.GetArgumentsForCallsMadeOn(action).Single();
			return args.First();
		}

	}
}