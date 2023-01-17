using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace YeSql.Net;

/// <summary>
/// Represents the loader that provides the functionality for loading SQL files into the system.
/// </summary>
public partial class YeSqlLoader
{
    /// <summary>
    ///  An instance of <see cref="YeSqlParser"/> class, used to parse SQL files before loading into the system.
    /// </summary>
    private readonly YeSqlParser _parser = new();

    /// <summary>
    ///  An instance of <see cref="YeSqlValidationResult"/> class, used to store the result of validating SQL files before loading them into the system.
    /// </summary>
    private readonly YeSqlValidationResult _validationResult = new();


    /// <summary>
    /// Loads all SQL files found in the root of the project.
    /// </summary>
    /// <returns>An instance of <see cref="IYeSqlCollection"/> interface that contains the loaded SQL files</returns>
    public IYeSqlCollection Load() 
        => Load("./sql");

    /// <summary>
    /// Loads the SQL files from the specified file paths.
    /// </summary>
    /// <param name="files">An array of file paths of the SQL files to be loaded.</param>
    /// <returns>An instance of <see cref="IYeSqlCollection"/> interface that contains the loaded SQL files.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the files parameter is null.</exception>
    public IYeSqlCollection Load(params string[] files)
    {
        if (files is null)
            throw new ArgumentNullException(nameof(files));

        var sqlFiles = GetSqlFileContents(files);

        foreach (var file in sqlFiles)
        {
            _parser.Parse(file.Content, file.FileName);
        }

        CreateAndThrowExceptions();

        return _parser.SqlStatements;
    }

    /// <summary>
    /// Loads the SQL files from the specified directory.
    /// </summary>
    /// <param name="directoryName">The path of the directory where the SQL files are located.</param>
    /// <returns>An instance of <see cref="IYeSqlCollection"/> interface that contains the loaded SQL files.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the directoryName parameter is null.</exception>
    /// <exception cref="AggregateException">Thrown when there are validation errors on the files or if no files were found in the specified directory.</exception>
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

        CreateAndThrowExceptions();

        return _parser.SqlStatements;
    }
}
