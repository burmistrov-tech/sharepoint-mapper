using System;
using System.Collections.Generic;
using BurmistrovTech.SharePoint.Mapper.Reflection;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class MappingOptions
    {
        public bool AllowFields { get; set; }
        public IEnumerable<ObjectConverter> Converters { get; set; }
    }
}