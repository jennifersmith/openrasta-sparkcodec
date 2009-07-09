using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

#region License information
// Taken from FluentNHibernate.org http://fluentnhibernate.org/
//Copyright (c) 2008-2009, James Gregory and contributors
//All rights reserved.
//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//  * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//  * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//  * Neither the name of James Gregory nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion
namespace ShoppingListViewer.Demo.Tests
{
	public static class ReflectionHelper
	{
		public static bool IsMethodExpression<TModel>(Expression<Func<TModel, object>> expression)
		{
			return expression.Body is MethodCallExpression;
		}

		public static bool IsPropertyExpression<TModel>(Expression<Func<TModel, object>> expression)
		{
			return getMemberExpression(expression, false) != null;
		}

		public static PropertyInfo GetProperty<TModel>(Expression<Func<TModel, object>> expression)
		{
			bool isExpressionOfDynamicComponent = expression.ToString().Contains("get_Item");

			if (isExpressionOfDynamicComponent)
				return GetDynamicComponentProperty(expression);

			MemberExpression memberExpression = getMemberExpression(expression);

			return (PropertyInfo) memberExpression.Member;
		}

		private static PropertyInfo GetDynamicComponentProperty<TModel>(Expression<Func<TModel, object>> expression)
		{
			Type desiredConversionType = null;
			MethodCallExpression methodCallExpression = null;
			Expression nextOperand = expression.Body;

			while (nextOperand != null)
			{
				if (nextOperand.NodeType == ExpressionType.Call)
				{
					methodCallExpression = nextOperand as MethodCallExpression;
					break;
				}

				if (nextOperand.NodeType != ExpressionType.Convert)
					throw new ArgumentException("Expression not supported", "expression");

				var unaryExpression = (UnaryExpression) nextOperand;
				desiredConversionType = unaryExpression.Type;
				nextOperand = unaryExpression.Operand;
			}

			var constExpression = methodCallExpression.Arguments[0] as ConstantExpression;

			return new DummyPropertyInfo((string) constExpression.Value, desiredConversionType);
		}

		public static PropertyInfo GetProperty<TModel, T>(Expression<Func<TModel, T>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);

			return (PropertyInfo) memberExpression.Member;
		}

		private static MemberExpression getMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression)
		{
			return getMemberExpression(expression, true);
		}

		private static MemberExpression getMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression, bool enforceCheck)
		{
			MemberExpression memberExpression = null;
			if (expression.Body.NodeType == ExpressionType.Convert)
			{
				var body = (UnaryExpression) expression.Body;
				memberExpression = body.Operand as MemberExpression;
			}
			else if (expression.Body.NodeType == ExpressionType.MemberAccess)
			{
				memberExpression = expression.Body as MemberExpression;
			}

			if (enforceCheck && memberExpression == null)
			{
				throw new ArgumentException("Not a member access", "member");
			}

			return memberExpression;
		}

		public static Accessor GetAccessor<TModel>(Expression<Func<TModel, object>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);

			return getAccessor(memberExpression);
		}

		private static Accessor getAccessor(MemberExpression memberExpression)
		{
			var list = new List<PropertyInfo>();

			while (memberExpression != null)
			{
				list.Add((PropertyInfo) memberExpression.Member);
				memberExpression = memberExpression.Expression as MemberExpression;
			}

			if (list.Count == 1)
			{
				return new SingleProperty(list[0]);
			}

			list.Reverse();
			return new PropertyChain(list.ToArray());
		}

		public static Accessor GetAccessor<TModel, T>(Expression<Func<TModel, T>> expression)
		{
			MemberExpression memberExpression = getMemberExpression(expression);

			return getAccessor(memberExpression);
		}

		public static MethodInfo GetMethod<T>(Expression<Func<T, object>> expression)
		{
			var methodCall = (MethodCallExpression) expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod<T, U>(Expression<Func<T, U>> expression)
		{
			var methodCall = (MethodCallExpression) expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod<T, U, V>(Expression<Func<T, U, V>> expression)
		{
			var methodCall = (MethodCallExpression) expression.Body;
			return methodCall.Method;
		}

		public static MethodInfo GetMethod(Expression<Func<object>> expression)
		{
			var methodCall = (MethodCallExpression) expression.Body;
			return methodCall.Method;
		}
	}

	public static class InvocationHelper
	{
		public static object InvokeGenericMethodWithDynamicTypeArguments<T>(T target, Expression<Func<T, object>> expression,
		                                                                    object[] methodArguments,
		                                                                    params Type[] typeArguments)
		{
			MethodInfo methodInfo = ReflectionHelper.GetMethod(expression);
			if (methodInfo.GetGenericArguments().Length != typeArguments.Length)
				throw new ArgumentException(
					string.Format(
						"The method '{0}' has {1} type argument(s) but {2} type argument(s) were passed. The amounts must be equal.",
						methodInfo.Name,
						methodInfo.GetGenericArguments().Length,
						typeArguments.Length));

			return methodInfo
				.GetGenericMethodDefinition()
				.MakeGenericMethod(typeArguments)
				.Invoke(target, methodArguments);
		}
	}

	internal sealed class DummyPropertyInfo : PropertyInfo
	{
		private readonly string _name;
		private readonly Type _type;

		public DummyPropertyInfo(string name, Type type)
		{
			if (name == null) throw new ArgumentNullException("name");
			if (type == null) throw new ArgumentNullException("type");

			_name = name;
			_type = type;
		}

		public override string Name
		{
			get { return _name; }
		}

		public override Type DeclaringType
		{
			get { throw new NotImplementedException(); }
		}

		public override Type ReflectedType
		{
			get { throw new NotImplementedException(); }
		}

		public override Type PropertyType
		{
			get { return _type; }
		}

		public override PropertyAttributes Attributes
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanRead
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanWrite
		{
			get { throw new NotImplementedException(); }
		}

		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotImplementedException();
		}

		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}

		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index,
		                                CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index,
		                              CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			throw new NotImplementedException();
		}

		public override ParameterInfo[] GetIndexParameters()
		{
			throw new NotImplementedException();
		}

		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotImplementedException();
		}
	}

	public interface Accessor
	{
		string FieldName { get; }

		Type PropertyType { get; }
		PropertyInfo InnerProperty { get; }
		string Name { get; }
		void SetValue(object target, object propertyValue);
		object GetValue(object target);

		Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression);
	}

	public class SingleProperty : Accessor
	{
		private readonly PropertyInfo _property;

		public SingleProperty(PropertyInfo property)
		{
			_property = property;
		}

		#region Accessor Members

		public string FieldName
		{
			get { return _property.Name; }
		}

		public Type PropertyType
		{
			get { return _property.PropertyType; }
		}

		public PropertyInfo InnerProperty
		{
			get { return _property; }
		}

		public Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty(expression);
			return new PropertyChain(new[] {_property, property});
		}

		public string Name
		{
			get { return _property.Name; }
		}

		public void SetValue(object target, object propertyValue)
		{
			if (_property.CanWrite)
			{
				_property.SetValue(target, propertyValue, null);
			}
		}

		public object GetValue(object target)
		{
			return _property.GetValue(target, null);
		}

		#endregion

		public static SingleProperty Build<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty(expression);
			return new SingleProperty(property);
		}

		public static SingleProperty Build<T>(string propertyName)
		{
			PropertyInfo property = typeof (T).GetProperty(propertyName);
			return new SingleProperty(property);
		}
	}

	public class PropertyChain : Accessor
	{
		private readonly PropertyInfo[] _chain;
		private readonly SingleProperty _innerProperty;

		public PropertyChain(PropertyInfo[] properties)
		{
			_chain = new PropertyInfo[properties.Length - 1];
			for (int i = 0; i < _chain.Length; i++)
			{
				_chain[i] = properties[i];
			}

			_innerProperty = new SingleProperty(properties[properties.Length - 1]);
		}

		#region Accessor Members

		public void SetValue(object target, object propertyValue)
		{
			target = findInnerMostTarget(target);
			if (target == null)
			{
				return;
			}

			_innerProperty.SetValue(target, propertyValue);
		}

		public object GetValue(object target)
		{
			target = findInnerMostTarget(target);

			if (target == null)
			{
				return null;
			}

			return _innerProperty.GetValue(target);
		}

		public string FieldName
		{
			get { return _innerProperty.FieldName; }
		}

		public Type PropertyType
		{
			get { return _innerProperty.PropertyType; }
		}

		public PropertyInfo InnerProperty
		{
			get { return _innerProperty.InnerProperty; }
		}

		public Accessor GetChildAccessor<T>(Expression<Func<T, object>> expression)
		{
			PropertyInfo property = ReflectionHelper.GetProperty(expression);
			var list = new List<PropertyInfo>(_chain);
			list.Add(_innerProperty.InnerProperty);
			list.Add(property);

			return new PropertyChain(list.ToArray());
		}

		public string Name
		{
			get
			{
				string returnValue = string.Empty;
				foreach (PropertyInfo info in _chain)
				{
					returnValue += info.Name;
				}

				returnValue += _innerProperty.Name;

				return returnValue;
			}
		}

		#endregion

		private object findInnerMostTarget(object target)
		{
			foreach (PropertyInfo info in _chain)
			{
				target = info.GetValue(target, null);
				if (target == null)
				{
					return null;
				}
			}

			return target;
		}
	}
}