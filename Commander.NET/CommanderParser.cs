﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

using Commander.NET.Attributes;
using Commander.NET.Exceptions;

namespace Commander.NET
{
    public static class CommanderParser
    {

		public static T Parse<T>(string[] args) where T : new()
		{

		}

		static IEnumerable<MemberInfo> GetParameterMembers<T, Q>() where Q : Attribute
		{
			foreach (MemberInfo member in typeof(T).GetProperties())
			{
				if (member.GetCustomAttribute<Q>() != null)
					yield return member;
			}
			foreach (MemberInfo member in typeof(T).GetFields())
			{
				if (member.GetCustomAttribute<Q>() != null)
					yield return member;
			}
		}

		static void SetValue<T>(T obj, MemberInfo member, object value)
		{
			if (member is PropertyInfo)
			{
				(member as PropertyInfo).SetValue(obj, value);
			}
			else if (member is FieldInfo)
			{
				(member as FieldInfo).SetValue(obj, value);
			}
		}

		static object GetDefaultValue<T>(MemberInfo member) where T : new()
		{
			if (member is PropertyInfo)
			{
				return (member as PropertyInfo).GetValue(new T());
			}
			else if (member is FieldInfo)
			{
				return (member as FieldInfo).GetValue(new T());
			}
			return null;
		}
    }
}
