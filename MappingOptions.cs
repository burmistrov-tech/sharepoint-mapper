using System.Collections.Generic;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class MappingOptions
    {
        public bool JustAttributes { get; set; }
        public IEnumerable<object> Converter { get; set; }
    }
}