using System;

namespace BurmistrovTech.SharePoint.Mapper.Tests
{
    public class TestDto
    {
        public int ID { get; set; }
        [FieldName("Title")]
        public string Title { get; set; }
        
        public string Status;
    }
}