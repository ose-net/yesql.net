using System;
using static YeSql.Net.FormattingMessage;

namespace YeSql.Net;

/// <summary>
/// Represents the parser that extracts the SQL statements from the tags.
/// </summary>
public partial class YeSqlParser
{
    private static readonly string[] s_newLines = new[] { "\r\n", "\n", "\r" };

    /// <summary>
    /// The maximum number of substrings to be returned by the Split method.
    /// </summary>
    private const int MaxCount = 2;

    /// <summary>
    /// This prefix is used to identify the SQL statement.
    /// </summary>
    private const string NamePrefix = "name:";

    /// <summary>
    /// The name of the SQL file that caused an error.
    /// </summary>
    private string _sqlFileName;

    /// <summary>
    /// Gets the dictionary containing the SQL statements that have been parsed from the data source (e.g., a SQL file).
    /// </summary>
    internal YeSqlDictionary SqlStatements { get; } = new();

    /// <summary>
    /// Gets the result of the validation performed on the SQL statements.
    /// </summary>
    internal YeSqlValidationResult ValidationResult { get; } = new();

    /// <summary>
    /// Start the parsing to extract the SQL statements from a data source.
    /// </summary>
    /// <param name="source">The data source to parsing.</param>
    /// <param name="sqlFileName">The name of the SQL file that caused an error.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>source</c> is <c>null</c>.</exception>
    internal ISqlCollection Parse(string source, string sqlFileName)
    {
        _sqlFileName = sqlFileName;
        return Parse(source, out _);
    }

    /// <inheritdoc cref="Parse(string, string)" />
    /// <exception cref="YeSqlParserException">
    /// If the parser encounters one or more errors.
    /// </exception>
    public ISqlCollection ParseAndThrow(string source)
    {
        var sqlStatements = Parse(source, out var validationResult);
        if (validationResult.HasError())
            throw new YeSqlParserException(validationResult.ErrorMessages);

        return sqlStatements;
    }

    /// <inheritdoc cref="Parse(string, string)" />
    /// <param name="source">The data source to parsing.</param>
    /// <param name="validationResult">The validation result of the parsing process.</param>
    public ISqlCollection Parse(string source, out YeSqlValidationResult validationResult)
    {
        if(source is null)
            throw new ArgumentNullException(nameof(source));

        validationResult = ValidationResult;
        if(string.IsNullOrWhiteSpace(source))
        {
            ValidationResult.Add(errorMessage: FormatParserExceptionMessage(
                ExceptionMessages.DataSourceIsEmptyOrWhitespace,
                sqlFileName: _sqlFileName
            ));
            return SqlStatements;
        }

        var lines = source.Split(s_newLines, StringSplitOptions.None);
        string currentTag = string.Empty;
        for (int i = 0, len = lines.Length; i < len; ++i)
        {
            var line = new Line { Number = i + 1, Text = lines[i] };

            if (string.IsNullOrWhiteSpace(line.Text))
                continue;

            if(IsCommentWithTag(ref line))
            {
                var extractedTag = ExtractTagName(ref line);
                AddExtractedTagToDictionary(extractedTag, ref line);
                currentTag = extractedTag;
                continue;
            }

            if (IsCommentWithoutTag(ref line))
                continue;

            if(string.IsNullOrEmpty(currentTag))
            {
                validationResult.Add(errorMessage: FormatParserExceptionMessage(
                    ExceptionMessages.LineIsNotAssociatedWithAnyTag,
                    actualValue: line.Text,
                    lineNumber: line.Number,
                    column: 1,
                    sqlFileName: _sqlFileName
                ));
                continue;
            }

            SqlStatements[currentTag] += line.Text.Trim() + Environment.NewLine;
        }

        return SqlStatements;
    }
}
