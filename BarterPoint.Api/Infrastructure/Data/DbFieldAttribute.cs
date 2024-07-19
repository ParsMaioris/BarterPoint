[AttributeUsage(AttributeTargets.Property)]
public class DbFieldAttribute : Attribute
{
    public string ColumnName { get; }

    public DbFieldAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}