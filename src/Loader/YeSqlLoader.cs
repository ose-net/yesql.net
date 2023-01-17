using System;

namespace YeSql.Net;

/// <summary>
/// Represents the loader that provides the functionality to load SQL files.
/// </summary>
public partial class YeSqlLoader
{
    /// <summary>
    ///  An instance of the <see cref="YeSqlParser"/> class used to parse SQL files.
    /// </summary>
    private readonly YeSqlParser _parser = new();

    /// <summary>
    ///  An instance of the <see cref="YeSqlValidationResult"/> class used to store errors associated with the loader.
    /// </summary>
    private readonly YeSqlValidationResult _validationResult = new();


    /// <summary>
    /// Loads SQL files from a default directory.
    /// </summary>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="AggregateException">If the parser and/or loader encounters one or more errors.</exception>
    public IYeSqlCollection Load() 
        => Load("./sql");

    /// <summary>
    /// Loads a set of SQL files.
    /// </summary>
    /// <param name="files">The SQL files to load.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>files</c> is <c>null</c>.</exception>
    /// <exception cref="AggregateException">If the parser and/or loader encounters one or more errors.</exception>
    public IYeSqlCollection Load(params string[] files)
    {
        if (files is null)
            throw new ArgumentNullException(nameof(files));

        var sqlFiles = GetSqlFileContents(files);

        foreach (var file in sqlFiles)
        {
            _parser.Parse(file.Content, file.FileName);
        }

        CreateAndThrowException();

        return _parser.SqlStatements;
    }

    /// <summary>
    /// Loads the SQL files from the specified directory.
    /// </summary>
    /// <param name="directoryName">The name of the directory where the SQL files are located.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>directoryName</c> is <c>null</c>.</exception>
    /// <exception cref="AggregateException">If the parser and/or loader encounters one or more errors.</exception>
    public IYeSqlCollection Load(string directoryName)
    {
        if (directoryName is null)
            throw new ArgumentNullException(nameof(directoryName));

        var sqlFiles = GetSqlFileContents(directoryName);

        if (sqlFiles.IsEmpty())
            _validationResult.Add(string.Format(ExceptionMessages.NoneFileFoundInSpecifiedDirectoryMessage, directoryName));

        foreach (var file in sqlFiles)
        {
            _parser.Parse(file.Content, file.FileName);
        }

        CreateAndThrowException();

        return _parser.SqlStatements;
    }
}
