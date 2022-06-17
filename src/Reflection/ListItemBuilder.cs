using System;
using Microsoft.SharePoint.Client;

namespace BurmistrovTech.SharePoint.Mapper.Reflection
{
    internal class ListItemBuilder
    {
        private readonly MappingOptions _options;
        private readonly ListItem _item;

        public ListItemBuilder(ListItem item, MappingOptions options)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public ListItemBuilder AssignProperties<T>(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            foreach (var property in typeof(T).GetWrappedProperties())
            {
                var value = property.Value.GetValue(entity);

                _item[property.Name] = value;
            }

            return this;
        }

        public ListItemBuilder AssignFields<T>(T entity)
        {
            if (!_options.AllowFields) return this;
            
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            foreach (var field in typeof(T).GetWrappedFields())
            {
                var value = field.Value.GetValue(entity);

                _item[field.Name] = value;
            }

            return this;
        }
    }
}