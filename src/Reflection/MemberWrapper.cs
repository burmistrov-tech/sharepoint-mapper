using System;
using System.Reflection;

namespace BurmistrovTech.SharePoint.Mapper.Reflection
{
    internal class MemberWrapper<T> where T : MemberInfo
    {
        public MemberWrapper(T member)
        {
            Value = member ?? throw new ArgumentNullException(nameof(member));
            Name = member.GetName();
            IgnoreCondition = member.GetIgnoreCondition();
            Converter = member.GetAdapter();
        }

        /// <summary>
        /// The value of T member
        /// </summary>
        public T Value { get; }
        public string Name { get; }
        public IgnoreCondition IgnoreCondition { get; }
        public ObjectConverter Converter { get; }

        public Type EntityType
        {
            get
            {
                switch (Value)
                {
                    case PropertyInfo property: return property.PropertyType;
                    case FieldInfo field: return field.FieldType;
                    default: return default;
                }
            }
        }
        
        public object GetValue(object obj)
        {
            switch (Value)
            {
                case PropertyInfo property: return property.GetValue(obj);
                case FieldInfo field: return field.GetValue(obj);
                default: throw new InvalidOperationException("Can get value only for a field or property");
            }
        }
        
        public void SetValue(object obj, object value)
        {
            switch (Value)
            {
                case PropertyInfo property: property.SetValue(obj, value); return;
                case FieldInfo field: field.SetValue(obj, value); return;
                default: throw new InvalidOperationException("Can set value only for a field or property");
            }
        }
    }
}