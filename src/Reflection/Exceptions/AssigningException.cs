using System;

namespace BurmistrovTech.SharePoint.Mapper
{
    public class AssigningException : Exception
    {
        public AssigningException(string message) : base(message) { }
        public AssigningException(string message, Exception exception) : base(message, exception) { }
    }
}