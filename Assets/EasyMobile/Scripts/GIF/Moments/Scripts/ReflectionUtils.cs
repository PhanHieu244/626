/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Moments
{
	public class ReflectionUtils<T> where T : class, new()
	{
		readonly T _Instance;

		public ReflectionUtils(T instance)
		{
			_Instance = instance;
		}

		public string GetFieldName<U>(Expression<Func<T, U>> fieldAccess)
		{
			MemberExpression memberExpression = fieldAccess.Body as MemberExpression;

			if (memberExpression != null)
				return memberExpression.Member.Name;

			throw new InvalidOperationException("Member expression expected");
		}

		public FieldInfo GetField(string fieldName)
		{
			return typeof(T).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public A GetAttribute<A>(FieldInfo field) where A : Attribute
		{
			return (A)Attribute.GetCustomAttribute(field, typeof(A));
		}

		// MinAttribute
		public void ConstrainMin<U>(Expression<Func<T, U>> fieldAccess, float value)
		{
			FieldInfo fieldInfo = GetField(GetFieldName(fieldAccess));
			fieldInfo.SetValue(_Instance, Mathf.Max(value, GetAttribute<MinAttribute>(fieldInfo).min));
		}

		public void ConstrainMin<U>(Expression<Func<T, U>> fieldAccess, int value)
		{
			FieldInfo fieldInfo = GetField(GetFieldName(fieldAccess));
			fieldInfo.SetValue(_Instance, (int)Mathf.Max(value, GetAttribute<MinAttribute>(fieldInfo).min));
		}

		// RangeAttribute
		public void ConstrainRange<U>(Expression<Func<T, U>> fieldAccess, float value)
		{
			FieldInfo fieldInfo = GetField(GetFieldName(fieldAccess));
			RangeAttribute attr = GetAttribute<RangeAttribute>(fieldInfo);
			fieldInfo.SetValue(_Instance, Mathf.Clamp(value, attr.min, attr.max));
		}

		public void ConstrainRange<U>(Expression<Func<T, U>> fieldAccess, int value)
		{
			FieldInfo fieldInfo = GetField(GetFieldName(fieldAccess));
			RangeAttribute attr = GetAttribute<RangeAttribute>(fieldInfo);
			fieldInfo.SetValue(_Instance, (int)Mathf.Clamp(value, attr.min, attr.max));
		}
	}
}
