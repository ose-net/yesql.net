using System;
using System.Collections.Generic;
using System.IO;

namespace YeSql.Net;

public partial class YeSqlLoader
{
    /// <summary>
    /// Throws an exception if there are errors.
    /// </summary>
    /// <exception cref="AggregateException">
    /// If the parser and/or loader encounters one or more errors.
    /// </exception>
    private void ThrowExceptionIfErrorsExist()
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
    /// Loads the SQL statements from a specified file.
    /// </summary>
    /// <param name="file">
    /// The SQL file to load (can include your path or not).
    /// </param>
    private Result<SqlFile> LoadFromFile(string file)
    {
        var path = Path.IsPathRooted(file) ? 
            file : 
            Path.Combine(AppContext.BaseDirectory, file);

        if (HasNotSqlExtension(path))
        {
            _validationResult.Add(string.Format(ExceptionMessages.FileHasNotSqlExtension, file));
            return Result<SqlFile>.Failure();
        }

        if (!File.Exists(path))
        {
            _validationResult.Add(string.Format(ExceptionMessages.FileNotFound, file));
            return Result<SqlFile>.Failure();
        }

        var sqlFile = new SqlFile
        {
            FileName = Path.GetFileName(file),
            Content  = File.ReadAllText(path)
        };
        return Result<SqlFile>.Success(sqlFile);
    }

    /// <summary>
    /// Checks if the file name has not sql extension.
    /// </summary>
    /// <param name="fileName">
    /// The file name to validate.
    /// </param>
    /// <returns>
    /// <c>true</c> if the file has not sql extension, otherwise <c>false</c>.
    /// </returns>
    private bool HasNotSqlExtension(string fileName)
        => !Path.GetExtension(fileName).Equals(".sql", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Loads the SQL statements from all the SQL files in the specified directory.
    /// </summary>
    /// <param name="directoryName">
    /// The name of the directory where the SQL files are located.
    /// </param>
    private Result<IEnumerable<SqlFile>> LoadFromDirectory(string directoryName)
    {
        var path = Path.IsPathRooted(directoryName) ?
            directoryName :
            Path.Combine(AppContext.BaseDirectory, directoryName);

        if (!Directory.Exists(path))
        {
            _validationResult.Add(string.Format(ExceptionMessages.DirectoryNotFound, directoryName));
            return Result<IEnumerable<SqlFile>>.Failure();
        }

        var sqlFiles = GetSqlFiles(path);
        if (sqlFiles.IsEmpty())
        {
            _validationResult.Add(string.Format(ExceptionMessages.NoneFileFoundInSpecifiedDirectory, directoryName));
            return Result<IEnumerable<SqlFile>>.Failure();
        }

        return Result<IEnumerable<SqlFile>>.Success(sqlFiles);
    }

    /// <summary>
    /// Returns the details of the SQL files in a specified directory.
    /// </summary>
    /// <param name="directoryName">
    /// The name of the directory where the SQL files are located.
    /// </param>
    /// <returns>
    /// An enumerable of type <see cref="SqlFile"/> that contains the SQL file details.
    /// </returns>
    private IEnumerable<SqlFile> GetSqlFiles(string directoryName)
    {
        var files = Directory.GetFiles(directoryName, "*.sql", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            yield return new()
            {
                FileName = Path.GetFileName(file),
                Content  = File.ReadAllText(file)
            };
        }
    }
}
