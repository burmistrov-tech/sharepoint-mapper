using System;

namespace BurmistrovTech.SharePoint.Mapper
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrDefault(this object value, Type type)
        {
            if (value == null) return true;

            if (type == null) throw new ArgumentNullException(nameof(type));
            
            if (!type.IsValueType) return false;
            
            if (Nullable.GetUnderlyingType(type) != null) return false;
            
            var defaultValue = Activator.CreateInstance(type);

            return defaultValue.Equals(value);
        }
    }
}