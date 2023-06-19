using System;
using System.Collections.Generic;
using System.IO;

namespace YeSql.Net;

public partial class YeSqlLoader
{

    /// <summary>
    /// Creates and throws <see cref="AggregateException" />.
    /// </summary>
    /// <exception cref="AggregateException">If the parser and/or loader encounters one or more errors.</exception>
    private void CreateAndThrowException()
    {
        var exceptions = new List<Exception>();

        if (_validationResult.HasError())
            exceptions.Add(new YeSqlLoaderException(_validationResult.ErrorMessages));

        if (_parser.ValidationResult.HasError())
            exceptions.Add(new YeSqlParserException(_parser.ValidationResult.ErrorMessages));

        if (exceptions.Count > 0)
            throw new AggregateException(exceptions);
    }


    /// <summary>
    /// Retrieves the details of the specified SQL files.
    /// </summary>
    /// <param name="files">The SQL files to load.</param>
    /// <returns>An enumerable of type <see cref="SqlFile"/> that contains the SQL file details.</returns>
    private IEnumerable<SqlFile> GetSqlFilesDetails(string[] files)
    {
        foreach (var file in files)
        {
            if (HasNotSqlExtension(file))
            {
                _validationResult.Add(string.Format(ExceptionMessages.FileHasNotSqlExtension, file));
                continue;
            }

            if(!File.Exists(file))
            {
                _validationResult.Add(string.Format(ExceptionMessages.FileNotFound, file));
                continue;
            }

            yield return new SqlFile
            {
                FileName = Path.GetFileName(file),
                Content  = File.ReadAllText(file)
            };
        }

    }

    /// <summary>
    /// Checks if the file name has not sql extension.
    /// </summary>
    /// <param name="fileName">The file name to validate.</param>
    /// <returns><c>true</c> if the file has not sql extension, otherwise <c>false</c>.</returns>
    private bool HasNotSqlExtension(string fileName)
        => !Path.GetExtension(fileName).Equals(".sql", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Retrieves the details of SQL files from a specified directory.
    /// </summary>
    /// <param name="directoryName">The name of the directory where the SQL files are located.</param>
    /// <returns>An enumerable of type <see cref="SqlFile"/> that contains the SQL file details.</returns>
    private IEnumerable<SqlFile> GetSqlFilesDetails(string directoryName)
    {
        var files = Directory.GetFiles(directoryName, "*.sql", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            yield return new SqlFile
            {
                Content  = File.ReadAllText(file),
                FileName = Path.GetFileName(file)
            };
        }
    }
}
