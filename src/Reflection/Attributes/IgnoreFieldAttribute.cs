using System;
using BurmistrovTech.SharePoint.Mapper.Reflection;

namespace BurmistrovTech.SharePoint.Mapper
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreFieldAttribute : Attribute
    {
        public IgnoreCondition Condition { get; set; } = IgnoreCondition.Always;
    }
    
}