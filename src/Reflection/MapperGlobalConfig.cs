using System.Collections.Generic;

namespace BurmistrovTech.SharePoint.Mapper.Reflection
{
    public static class MapperGlobalConfig
    {
        public static List<ObjectConverter> Converters { get; } = new List<ObjectConverter>();
    }
}