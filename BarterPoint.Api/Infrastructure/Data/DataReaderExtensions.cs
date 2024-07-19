using System.Data;

public static class DataReaderExtensions
{
    public static T MapTo<T>(this IDataReader reader) where T : new()
    {
        T obj = new T();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttributes(typeof(DbFieldAttribute), false).FirstOrDefault() as DbFieldAttribute;
            if (attribute != null)
            {
                var columnName = attribute.ColumnName;
                if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                {
                    property.SetValue(obj, Convert.ChangeType(reader[columnName], property.PropertyType));
                }
            }
        }

        return obj;
    }
}