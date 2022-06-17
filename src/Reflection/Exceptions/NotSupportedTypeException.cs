using System;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class NotSupportedTypeException : Exception
    {
        public NotSupportedTypeException(string message) : base(message) { }
    }
}