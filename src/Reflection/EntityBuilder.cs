using System;
using System.Linq;
using System.Reflection;
using Microsoft.SharePoint.Client;

namespace BurmistrovTech.SharePoint.Mapper.Reflection
{
    internal class EntityBuilder<T>
    {
        private readonly MappingOptions _options;
        private readonly T _entity;

        public EntityBuilder(MappingOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _entity = Activator.CreateInstance<T>();
        }

        public EntityBuilder<T> AssignProperties(ListItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            
            foreach (var property in typeof(T).GetWrappedProperties())
            {
                if (property.IgnoreCondition == IgnoreCondition.Never) continue;
                
                try
                {
                    var value = item[property.Name];
                    
                    if (property.IgnoreCondition == IgnoreCondition.NullOrDefault 
                        && value.IsNullOrDefault(property.Value.PropertyType)) continue;

                    var converter = property.Converter 
                        ?? _options.Converters.FirstOrDefault(c => c.EntityType == property.EntityType)
                        ?? MapperGlobalConfig.Converters.FirstOrDefault(c => c.EntityType == property.EntityType);

                    if (converter == null)
                    {
                        property.SetValue(_entity, value);
                        continue;
                    }
                    
                    var convertedValue = converter.ConvertItemObject(value);
                    
                    property.SetValue(_entity, convertedValue);
                }
                catch (Exception e)
                {
                    throw new AssigningException($"Error while assigning property {property.Name} for {typeof(T).Name}", e);
                }
            }

            return this;
        }

        public EntityBuilder<T> AssignFields(ListItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            
            if (!_options.AllowFields) return this;
            
            foreach (var field in typeof(T).GetWrappedFields())
            {
                
                try
                {
                    var value = item[field.Name];
                    
                    field.SetValue(_entity, value);
                }
                catch (Exception e)
                {
                    throw new AssigningException($"Error while assigning field {field.Name} for {typeof(T).Name}", e);
                }
            }
            
            return this;
        }

        public void Assign<T>(ListItem item) where T : MemberInfo
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            if (typeof(T) == typeof(FieldInfo) && !_options.AllowFields) return;
            
            foreach (var member in typeof(T).GetWrappedProperties())
            {
                if (member.IgnoreCondition == IgnoreCondition.Never) continue;
                
                try
                {
                    var value = item[member.Name];
                    
                    if (member.IgnoreCondition == IgnoreCondition.NullOrDefault 
                        && value.IsNullOrDefault(member.Value.PropertyType)) continue;

                    member.SetValue(_entity, value);
                }
                catch (Exception e)
                {
                    throw new AssigningException($"Error while assigning property {member.Name} for {typeof(T).Name}", e);
                }
            }
        }
        
        public T Build()
        {
            return _entity;
        }

        private bool ContainsEntityType(ObjectConverter converter, MemberWrapper<MemberInfo> wrapper)
        {
            return converter.EntityType == wrapper.EntityType;
        }
    }
}