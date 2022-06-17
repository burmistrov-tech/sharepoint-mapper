using System;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class ListItemFieldNameAttribute : Attribute
    {
        public string Name { get; }

        public ListItemFieldNameAttribute(string name)
        {
            Name = name;
        }
    }
}