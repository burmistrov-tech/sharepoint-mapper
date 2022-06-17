using System;
using System.Reflection;
using BurmistrovTech.SharePoint.Mapper.Reflection;

namespace BurmistrovTech.SharePoint.Mapper
{
    internal static class MemberInfoExtensions
    {
        public static IgnoreCondition GetIgnoreCondition(this MemberInfo member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            var attribute = (IgnoreFieldAttribute) Attribute.GetCustomAttribute(member, typeof(IgnoreFieldAttribute));

            return attribute?.Condition ?? IgnoreCondition.Never;
        }

        public static string GetName(this MemberInfo member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            if (!Attribute.IsDefined(member, typeof(FieldNameAttribute))) return member.Name;

            var attribute = (FieldNameAttribute) Attribute.GetCustomAttribute(member, typeof(FieldNameAttribute));

            return attribute.Name;
        }

        public static ObjectConverter GetAdapter(this MemberInfo member)
        {
            if (member == null) throw new ArgumentNullException(nameof(member));

            var attribute = (FieldAdapterAttribute) Attribute.GetCustomAttribute(member, typeof(FieldAdapterAttribute));

            if (attribute == null) return null;

            return (ObjectConverter) Activator.CreateInstance(attribute.AdapterType);
        }
    }
}