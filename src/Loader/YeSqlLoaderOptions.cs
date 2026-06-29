namespace YeSql.Net;

internal class YeSqlLoaderOptions
{
    public HashSet<string> ExcludedFileNames { get; } = new(StringComparer.OrdinalIgnoreCase);
}
