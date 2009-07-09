using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace ShoppingListViewer.Demo.Tests
{
	public class AutoObjectBuilder<TTarget>
		where TTarget : class, new()
	{
		Dictionary<string, AccessorWithValue> accessorWithValuesByName = new Dictionary<string, AccessorWithValue>();
		public AutoObjectBuilder<TTarget> With<TValue> ( Expression<Func<TTarget, TValue>> propertyGetter, TValue value)
		{
			Accessor accessor = ReflectionHelper.GetAccessor(propertyGetter);
			accessorWithValuesByName[accessor.Name] = new AccessorWithValue(value, accessor);
			return this;
		}
		public AutoObjectBuilder<TTarget> WithTrue(Expression<Func<TTarget, bool>> propertyGetter)
		{
			return With(propertyGetter, true);
		}
		public AutoObjectBuilder<TTarget> WithFalse(Expression<Func<TTarget, bool>> propertyGetter)
		{
			return With(propertyGetter, false);
		}
		public TTarget Build()
		{
			TTarget target = new TTarget();
			foreach (var accessorWithValue in accessorWithValuesByName.Values)
			{
				accessorWithValue.Accessor.SetValue(target, accessorWithValue.Value);
			}
			return target;
		}
		public void Describe(StringWriter writer)
		{
			writer.WriteLine("Object type " + typeof(TTarget));
			foreach (AccessorWithValue accessorWithValue in accessorWithValuesByName.Values)
			{
				writer.WriteLine("{0}={1}", accessorWithValue.Accessor.Name, accessorWithValue.Value);
			}
		}

		private class AccessorWithValue
		{
			private readonly object value;
			private readonly Accessor accessor;

			public AccessorWithValue(object value, Accessor accessor)
			{
				this.value = value;
				this.accessor = accessor;
			}

			public Accessor Accessor
			{
				get { return accessor; }
			}

			public object Value
			{
				get { return value; }
			}
		}


		public static AutoObjectBuilder<TTarget> Create()
		{
			return new AutoObjectBuilder<TTarget>();
		}
	}
}