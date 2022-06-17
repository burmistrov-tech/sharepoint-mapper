using System;

namespace BurmistrovTech.SharePoint.Mapper.Reflection
{
    public class ObjectConverter
    {
        internal ObjectConverter(Type entityType)
        {
            EntityType = entityType;
        }

        public Type EntityType { get; }

        public Func<object, object> ConvertEntityObject { get; set; }

        public Func<object, object> ConvertItemObject { get; set; }
    }
}