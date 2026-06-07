namespace YeSql.Net;

internal class PathResolver
{
    public static string BaseDirectory
    {
        get
        {
            // AppContext.BaseDirectory may not be available in all hosting
            // environments (e.g. SampSharp/open.mp). Fall back to the current
            // working directory when necessary.
            if (!string.IsNullOrWhiteSpace(AppContext.BaseDirectory))
                return AppContext.BaseDirectory;

            return Directory.GetCurrentDirectory();
        }
    }
}
