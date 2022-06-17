using System;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class FieldNameAttribute : Attribute
    {
        public string Name { get; }

        public FieldNameAttribute(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}