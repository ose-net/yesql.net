using System;
using System.Collections.Generic;
using System.IO;

namespace YeSql.Net;

public partial class YeSqlLoader
{

    /// <summary>
    /// Represents a class that is used to store the file name and contents of an SQL file.
    /// </summary>
    private class SqlFiles
    {
        /// <summary>
        /// Gets or sets the file name of the SQL file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the contents of the SQL file.
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// Checks for any validation errors that may have occurred during the loading process, and throws an exception if there are any.
    /// If there is validation errors found in <see cref="YeSqlLoader"/>, it throws an <see cref="YeSqlLoaderException"/>,
    /// If there is validation errors found in <see cref="YeSqlParser"/>, it throws an <see cref="YeSqlParserException"/>,
    /// If there is validation errors found in both <see cref="YeSqlLoader"/> and <see cref="YeSqlParser"/>, it throws an <see cref="AggregateException"/> that contains both exception.
    /// </summary>
    /// <exception cref="YeSqlLoaderException">Thrown when there are validation errors in <see cref="YeSqlLoader"/>.</exception>
    /// <exception cref="YeSqlParserException">Thrown when there are validation errors in <see cref="YeSqlParser"/>.</exception>
    /// <exception cref="AggregateException">Thrown when there are validation errors in <see cref="YeSqlLoader"/> and <see cref="YeSqlParser"/>.</exception>
    private void CreateAndThrowExceptions()
    {
        var exceptions = new List<Exception>();

        if (_validationResult.HasError())
            exceptions.Add(new YeSqlLoaderException(_validationResult.ErrorMessages));

        if (_parser.ValidationResult.HasError())
            exceptions.Add(new YeSqlParserException(_parser.ValidationResult.ErrorMessages));

        if (exceptions.Count == 0)
            throw new AggregateException(exceptions);
    }


    /// <summary>
    /// Retrieves the contents of the specified SQL files and returns them as an enumerable collection of <see cref="SqlFile"/> objects.
    /// </summary>
    /// <param name="files">An array of file paths of the SQL files to be loaded.</param>
    /// <returns>An enumerable collection of <see cref="SqlFile"/> objects that contains the file name and contents of the SQL files.</returns>
    private IEnumerable<SqlFile> GetSqlFileContents(string[] files)
    {
        foreach (var file in files)
        {
            if (file is null)
            {
                _validationResult.Add($"{nameof(files)} is null");
                continue;
            }

            var fileInfo = new FileInfo(file);
            var content = string.Empty;

            try
            {
                content = File.ReadAllText(file);
            }
            catch (FileNotFoundException)
            {
                _validationResult.Add(string.Format(ExceptionMessages.FileNotFoundMessage, file));
            }

            if (string.IsNullOrWhiteSpace(content))
                _validationResult.Add(ExceptionMessages.DataSourceIsEmptyOrWhitespaceMessage);

            if (fileInfo.Extension != "sql")
                _validationResult.Add("The file is not sql.");

            yield return new SqlFile
            {
                FileName = fileInfo.Name,
                Content = content
            };
        }

    }

    /// <summary>
    /// Retrieves the contents of the SQL files located in the specified directory and its subdirectories, and returns them as an enumerable collection of <see cref="SqlFile"/> objects.
    /// </summary>
    /// <param name="directoryName">The path of the directory where the SQL files are located.</param>
    /// <returns>An enumerable collection of <see cref="SqlFile"/> objects that contains the file name and contents of the SQL files.</returns>
    private IEnumerable<SqlFile> GetSqlFileContents(string directoryName)
    {
        var files = Directory.GetFiles(directoryName, "*.sql", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var name = new FileInfo(file).Name;

            if (string.IsNullOrWhiteSpace(content))
            {
                _validationResult.Add($"{name} {ExceptionMessages.DataSourceIsEmptyOrWhitespaceMessage}");
            }

            yield return new SqlFile
            {
                Content = content,
                FileName = name
            };
        }
    }
}
