using System;
using BurmistrovTech.SharePoint.Mapper.Reflection;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class FieldAdapterAttribute : Attribute
    {
        public Type AdapterType { get; }

        public FieldAdapterAttribute(Type adapterType)
        {
            if (adapterType == null) throw new ArgumentNullException(nameof(adapterType));
            if (!adapterType.IsSubclassOf(typeof(ObjectConverter<>))) throw new ArgumentException("");

            AdapterType = adapterType;
        }
    }
}