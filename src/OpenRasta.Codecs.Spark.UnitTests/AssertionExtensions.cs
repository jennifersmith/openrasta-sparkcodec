using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace OpenRasta.Codecs.Spark.UnitTests
{
	public static class AssertionExtensions
	{

		public static T As<T>(this object val)
		{
			val.ShouldBe<T>();
			return (T) val;
		}
		public static void ShouldBe<T>(this object val)
		{
			Assert.That(val, Is.InstanceOf(typeof(T)));
		}
		public static void ShouldContain<T>(this IEnumerable<T> list, T value)
		{
			list.ShouldNotBeNull();
			Assert.That(list.Contains(value), "Item not found in list");
		}
		public static void ShouldOnlyContain<T>(this IEnumerable<T> list, T value)
		{
			list.ShouldNotBeNull();
			Assert.That(list, Is.EquivalentTo(new[] {value}), "Expected {0} only in list, found {1}", value, list);
		}

		public static void ShouldStartWith<T>(this IEnumerable<T> list, IEnumerable<T> expected)
		{
			var startOfList = list.Take(expected.Count()).ToArray();
			list.ShouldNotBeNull();
			Assert.That(startOfList, Is.EqualTo(expected));
		}
		public static void ShouldEndWith<T>(this IEnumerable<T> list, IEnumerable<T> expected)
		{
			var endOfList = list.Skip(expected.Count());
			list.ShouldNotBeNull();
			Assert.That(endOfList, Is.EqualTo(list));
		}

		public static T ShouldContain<T>(this IEnumerable<T> list, Func<T, bool> predicate)
		{
			T result = list.Where(predicate).FirstOrDefault();
			Assert.That(result, Is.Not.Null, "Item not found in list");
			return result;
		}

		public static void CollectionShouldEqual<T>(this IEnumerable<T> value, IEnumerable<T> shouldEqual)
		{
			Assert.That(value, Is.EquivalentTo(shouldEqual));
		}

		public static void ShouldEqual<T>(this T value, T shouldEqual)
		{
			Assert.That(value, Is.EqualTo(shouldEqual));
		}

		public static void ShouldBeAtLeast<T>(this T value, T shouldEqual)
			where T : IComparable
		{
			Assert.That(value, Is.GreaterThanOrEqualTo(shouldEqual));
		}

		public static void ShouldBeTrueForAll<T>(this IEnumerable<T> values, Func<T, bool> eval)
		{
			values.Where(eval).Count().ShouldEqual(values.Count());
		}

		public static void ShouldBeTrueForOne<T>(this IEnumerable<T> values, Func<T, bool> eval)
		{
			values.Where(eval).Count().ShouldEqual(1);
		}

		public static void ShouldBeTrueForAtLeastOne<T>(this IEnumerable<T> values, Func<T, bool> eval)
		{
			values.Where(eval).Count().ShouldBeAtLeast(1);
		}

		public static void ShouldNotContain<T>(this IEnumerable<T> values, Func<T, bool> eval)
		{
			values.ShouldBeTrueForAll(x => !eval(x));
		}

		public static void ShouldEqualDate(this DateTime? value, DateTime shouldEqual)
		{
			value.ShouldNotBeNull();
			value.Value.Date.ShouldEqual(shouldEqual.Date);
		}

		public static void ShouldEqual<T>(this T? value, T shouldEqual)
			where T : struct
		{
			value.ShouldNotBeNull();
			Assert.That(value.Value, Is.EqualTo(shouldEqual));
		}

		public static void ShouldFallBetween<T>(this T value, T lowerLimit, T upperLimit, string message)
			where T : IComparable
		{
			Assert.That(value, Is.InRange(lowerLimit, upperLimit), message);
		}

		public static void IsDayOfWeek(this DateTime value, DayOfWeek dayOfWeek)
		{
			Assert.That(value.DayOfWeek, Is.EqualTo(dayOfWeek), "Incorrect day of the week");
		}

		public static void IsInYear(this DateTime value, int year)
		{
			Assert.That(value.Year, Is.EqualTo(year), "Date is not in the year " + year);
		}

		public static void IsWithinMonths(this DateTime value, int lowerMonth, int upperMonth)
		{
			value.Month.ShouldFallBetween(lowerMonth, upperMonth, "Does not fall between correct months");
		}

		public static TException ShouldThrow<TException>(Action action)
			where TException : Exception
		{
			return Assert.Throws<TException>(() => action());
		}

		public static void ShouldBeNull(this Object val)
		{
			Assert.That(val, Is.Null);
		}

		public static void ShouldBeEmpty(this Object val)
		{
			Assert.That(val, Is.Empty);
		}

		public static void ShouldNotBeNull(this Object val)
		{
			Assert.That(val, Is.Not.Null);
		}

		public static void ShouldBeFalse(this bool val)
		{
			val.ShouldEqual(false);
		}

		public static void ShouldBeTrue(this bool val)
		{
			val.ShouldEqual(true);	
		}
		public static void ShouldHaveCount<T>(this IEnumerable<T> val, int expected)
		{
			Assert.That(val.ToArray(), Has.Length.EqualTo(expected));
		}
	}
}