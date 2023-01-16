using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace YeSql.Net;

public class YeSqlLoader
{
    private readonly YeSqlParser _parser = new();

    private readonly YeSqlValidationResult _validationResult = new();

    public IYeSqlCollection Load()
    {
        return Load(Directory.GetCurrentDirectory());
    }

    public IYeSqlCollection Load(params string[] files)
    {
        if (files is null)
            throw new ArgumentNullException($"{nameof(files)} is null");

        var filesSql = GetSqlFileContents(files);

        foreach (var file in filesSql)
        {
            _parser.SqlFileName = file.FileName;
            _parser.Parse(file.Content, out _);
        }

        CreateAndThrowExceptions();

        return _parser.SqlStatements;
    }

    public IYeSqlCollection Load(string directoryName)
    {
        if (directoryName is null)
            throw new ArgumentNullException($"{nameof(directoryName)} is null");

        var files = GetSqlFileContents(directoryName);

        if (!files.Any())
            _validationResult.Add(string.Format(ExceptionMessages.NoneFileFoundInSpecifiedDirectoryMessage, directoryName));

        if (_validationResult.HasError())
            throw new AggregateException();
        
        foreach (var file in files)
        {
            _parser.SqlFileName = file.FileName;
            _parser.Parse(file.Content, out _);         
        }

        CreateAndThrowExceptions();

        return _parser.SqlStatements;
    }

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

    public IYeSqlCollection Load(string[] files)
    {
        // Al final del método...
        // ...

        CreateAndThrowExceptions();
    }

    private bool IsSqlFile(string fileName)
    => new FileInfo(fileName).Extension == "sql";

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

    private IEnumerable<SqlFile> GetSqlFileContents(string directoryName)
    {
        var files = Directory.GetFiles(directoryName, "*.sql", SearchOption.AllDirectories);

        foreach(var file in files)
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

    private class SqlFile
    {
        public string FileName { get; set; }
        public string Content { get; set; }
    }

}
