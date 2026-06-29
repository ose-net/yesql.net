namespace YeSql.Net;

public partial class YeSqlLoader
{
    private readonly YeSqlLoaderOptions _options = new();

    /// <summary>
    /// Configures the SQL files to exclude when calling
    /// <see cref="LoadFromDirectories(string[])"/>.
    /// </summary>
    /// <remarks>
    /// The specified file names are compared against the SQL file names found
    /// in the configured directories. Directory paths are ignored.
    /// </remarks>
    /// <param name="sqlFiles">
    /// The names of the SQL files to exclude.
    /// </param>
    /// <returns>
    /// The current <see cref="YeSqlLoader"/> instance so that additional
    /// configuration can be chained.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="sqlFiles"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// One or more file names in <paramref name="sqlFiles"/> is null, empty,
    /// or consists only of white-space characters.
    /// </exception>
    public YeSqlLoader Exclude(params string[] sqlFiles)
    {
        ThrowHelper.ThrowIfNull(sqlFiles, nameof(sqlFiles));
        ThrowHelper.ThrowIfContainsNullOrWhiteSpace(sqlFiles, nameof(sqlFiles));

        foreach (string sqlFile in sqlFiles)
            _options.ExcludedFileNames.Add(Path.GetFileName(sqlFile));

        return this;
    }
}
