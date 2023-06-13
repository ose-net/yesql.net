using System;

namespace YeSql.Net;

/// <summary>
/// Represents the loader that provides the functionality to load SQL files.
/// </summary>
public partial class YeSqlLoader
{
    /// <summary>
    /// An instance of the <see cref="YeSqlParser"/> class used to parse SQL files.
    /// </summary>
    private readonly YeSqlParser _parser = new();

    /// <summary>
    /// An instance of the <see cref="YeSqlValidationResult"/> class used to store errors associated with the loader.
    /// </summary>
    private readonly YeSqlValidationResult _validationResult = new();

    /// <summary>
    /// Loads the SQL statements from the specified files.
    /// </summary>
    /// <param name="sqlFiles">The SQL files to load.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>sqlFiles</c> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// One or more files in <paramref name="sqlFiles"/> is null, empty or consists only of white-space characters.
    /// -or-
    /// The length of the <paramref name="sqlFiles"/> list is zero.
    /// </exception>
    /// <exception cref="AggregateException">If the parser and/or loader encounters one or more errors.</exception>
    public IYeSqlCollection LoadFromFiles(params string[] sqlFiles)
    {
        if (sqlFiles is null)
            throw new ArgumentNullException(nameof(sqlFiles));

        if (sqlFiles.IsEmpty())
            throw new ArgumentException(ExceptionMessages.LengthOfParamsListIsZero);
      
        if (sqlFiles.ContainsNullOrWhiteSpace())
            throw new ArgumentException(string.Format(ExceptionMessages.CollectionHasNullValueOrOnlyWhitespace, nameof(sqlFiles)));

        var sqlFilesDetails = GetSqlFilesDetails(sqlFiles);

        foreach (var fileDetails in sqlFilesDetails)
            _parser.Parse(fileDetails.Content, fileDetails.FileName);

        CreateAndThrowException();
        return _parser.SqlStatements;
    }

    /// <summary>
    /// Loads the SQL statements from all the SQL files in the specified directories.
    /// </summary>
    /// <param name="directories">A set of directories where the SQL files are located.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>directories</c> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// One or more directories in <paramref name="directories"/> is null, empty or consists only of white-space characters.
    /// -or-
    /// The length of the <paramref name="directories"/> list is zero.
    /// </exception>
    /// <exception cref="AggregateException">If the parser and/or loader encounters one or more errors.</exception>
    public IYeSqlCollection LoadFromDirectories(params string[] directories)
    {
        if (directories is null)
            throw new ArgumentNullException(nameof(directories));

        if (directories.IsEmpty())
            throw new ArgumentException(ExceptionMessages.LengthOfParamsListIsZero);

        if(directories.ContainsNullOrWhiteSpace())
            throw new ArgumentException(string.Format(ExceptionMessages.CollectionHasNullValueOrOnlyWhitespace, nameof(directories)));

        foreach (var directory in directories)
            LoadFromDirectory(directory);

        CreateAndThrowException();
        return _parser.SqlStatements;
    }

    /// <summary>
    /// Loads the SQL statements from all the SQL files in the specified directory.
    /// </summary>
    /// <param name="directoryName">The name of the directory where the SQL files are located.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>directoryName</c> is <c>null</c>.</exception>
    private IYeSqlCollection LoadFromDirectory(string directoryName)
    {
        if (directoryName is null)
            throw new ArgumentNullException(nameof(directoryName));

        var sqlFilesDetails = GetSqlFilesDetails(directoryName);

        if (sqlFilesDetails.IsEmpty())
            _validationResult.Add(string.Format(ExceptionMessages.NoneFileFoundInSpecifiedDirectory, directoryName));

        foreach (var fileDetails in sqlFilesDetails)
            _parser.Parse(fileDetails.Content, fileDetails.FileName);

        return _parser.SqlStatements;
    }
}
