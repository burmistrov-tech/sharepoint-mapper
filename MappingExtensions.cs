using System;
using System.Linq;
using System.Reflection;
using Microsoft.SharePoint.Client;

namespace BurmistrovTech.SharePoint.Mapper
{
    public static class MappingExtensions
    {
        public static T Map<T>(this ListItem item)
        {
            var properties = typeof(T).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ListItemFieldNameAttribute)))
                .ToDictionary(SelectAttributesFromProperty, prop => prop);

            if (!properties.Any())
            {
                throw new Exception($"Properties with the {nameof(ListItemFieldNameAttribute)}" +
                                    $" attribute were not found for {typeof(T).FullName}");
            }
            
            var entity = Activator.CreateInstance<T>();

            foreach (var property in properties)
            {
                try
                {
                    var value = item[property.Key];
                    
                    property.Value.SetValue(entity, value);
                }
                catch (Exception e)
                {
                    throw new Exception($"Error while assigning properties for {entity.GetType().FullName}", e);
                }
            }

            return entity;
        }
        
        private static string SelectAttributesFromProperty(PropertyInfo prop)
        {
            var baseAttribute = Attribute.GetCustomAttribute(prop, typeof(ListItemFieldNameAttribute));

            var attribute = (ListItemFieldNameAttribute) baseAttribute;

            return attribute.Name;
        }
    }
}