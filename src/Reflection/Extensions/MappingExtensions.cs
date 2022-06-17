using System;
using System.Linq.Expressions;
using BurmistrovTech.SharePoint.Mapper.Reflection;
using Microsoft.SharePoint.Client;

namespace BurmistrovTech.SharePoint.Mapper
{
    public static class MappingExtensions
    {
        public static T Map<T>(this ListItem item, MappingOptions options, params Expression<Func<T, object>>[] retrievals)
        {
            return new EntityBuilder<T>(options)
                .AssignProperties(item)
                .AssignFields(item)
                .Build();
        }
    }
}