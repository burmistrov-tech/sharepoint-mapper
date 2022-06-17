using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BurmistrovTech.SharePoint.Mapper.Reflection;

namespace BurmistrovTech.SharePoint.Mapper
{
    public static class TypeExtensions
    {
        internal static IEnumerable<MemberWrapper<PropertyInfo>> GetWrappedProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public).Select(prop => new MemberWrapper<PropertyInfo>(prop));
        }
        
        internal static IEnumerable<MemberWrapper<FieldInfo>> GetWrappedFields(this Type type)
        {
            return type.GetFields(BindingFlags.Public).Select(field => new MemberWrapper<FieldInfo>(field));
        }
        
        internal static IEnumerable<MemberWrapper<T>> GetWrapped<T>(this Type type) where T : MemberInfo
        {
            return type.GetMembers(BindingFlags.Public)
                .Where(member => member is T)
                .Cast<T>().Select(member => new MemberWrapper<T>(member));
        }
    }
}