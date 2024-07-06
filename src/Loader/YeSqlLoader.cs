using System;
using System.IO;

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
    private readonly YeSqlValidationResult _validationResult = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlLoader"/> class.
    /// </summary>
    public YeSqlLoader() { }

    /// <summary>
    /// Loads the SQL statements from a default directory called <c>yesql</c>.
    /// </summary>
    /// <remarks>
    /// This method starts searching from the current directory 
    /// where the application is running (e.g., bin/Debug/net8.0).
    /// </remarks>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="AggregateException">
    /// If the parser and/or loader encounters one or more errors.
    /// </exception>
    public ISqlCollection LoadFromDefaultDirectory() 
        => LoadFromDirectories("yesql");

    /// <summary>
    /// Loads the SQL statements from the specified files.
    /// </summary>
    /// <remarks>
    /// The <paramref name="sqlFiles"/> parameter can include the absolute or relative path along with the file name. 
    /// <para>
    /// If the path is relative, the method will start searching 
    /// from the current directory where the application is running (e.g., bin/Debug/net8.0).
    /// </para>
    /// </remarks>
    /// <param name="sqlFiles">The SQL files to load.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException">
    /// <c>sqlFiles</c> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// One or more files in <paramref name="sqlFiles"/> is null, empty or consists only of white-space characters.
    /// </exception>
    /// <exception cref="AggregateException">
    /// If the parser and/or loader encounters one or more errors.
    /// </exception>
    public ISqlCollection LoadFromFiles(params string[] sqlFiles)
    {
        ThrowHelper.ThrowIfNull(sqlFiles, nameof(sqlFiles));
        if (sqlFiles.IsEmpty())
            return _parser.SqlStatements;

        ThrowHelper.ThrowIfContainsNullOrWhiteSpace(sqlFiles, nameof(sqlFiles));
        foreach (var fileName in sqlFiles)
        {
            Result<SqlFile> result = LoadFromFile(fileName);
            if (result.IsSuccess)
                _parser.Parse(result.Value.Content, result.Value.FileName);
        }

        ThrowExceptionIfErrorsExist();
        return _parser.SqlStatements;
    }

    /// <summary>
    /// Loads the SQL statements from all the SQL files in the specified directories.
    /// </summary>
    /// <remarks>
    /// The <paramref name="directories"/> parameter can include the absolute or relative path along with the directory name. 
    /// <para>
    /// If the path is relative, the method will start searching 
    /// from the current directory where the application is running (e.g., bin/Debug/net8.0).
    /// </para>
    /// </remarks>
    /// <param name="directories">A set of directories where the SQL files are located.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException">
    /// <c>directories</c> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// One or more directories in <paramref name="directories"/> is null, empty or consists only of white-space characters.
    /// </exception>
    /// <exception cref="AggregateException">
    /// If the parser and/or loader encounters one or more errors.
    /// </exception>
    public ISqlCollection LoadFromDirectories(params string[] directories)
    {
        ThrowHelper.ThrowIfNull(directories, nameof(directories));
        if (directories.IsEmpty())
            return _parser.SqlStatements;

        ThrowHelper.ThrowIfContainsNullOrWhiteSpace(directories, nameof(directories));
        foreach (var directory in directories)
        {
            Result<IEnumerable<SqlFile>> result = LoadFromDirectory(directory);
            if (result.IsFailed)
                continue;

            foreach (SqlFile sqlFile in result.Value)
                _parser.Parse(sqlFile.Content, sqlFile.FileName);
        }

        ThrowExceptionIfErrorsExist();
        return _parser.SqlStatements;
    }
}
