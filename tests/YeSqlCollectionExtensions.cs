namespace YeSql.Net.Tests;

public static class YeSqlCollectionExtensions
{
    public static Dictionary<string, string> ToDictionary(this IYeSqlCollection collection)
        => collection.ToDictionary(model => model.Name, model => model.SqlStatement);
}
