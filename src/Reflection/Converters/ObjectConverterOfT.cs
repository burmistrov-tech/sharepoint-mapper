namespace BurmistrovTech.SharePoint.Mapper.Reflection
{
    public class ObjectConverter<T>
    {
        public virtual T Convert(object value)
        {
            return (T) value;
        }
        
        public object Convert(T value)
        {
            return (object) value;
        }

        public static implicit operator ObjectConverter(ObjectConverter<T> converter)
        {
            var genericType = converter.GetType().GetGenericTypeDefinition();
            
            return new ObjectConverter(genericType)
            {
                ConvertEntityObject = value => converter.Convert(value),
                ConvertItemObject = value =>  converter.Convert((T) value)
            };
        }
    }
}