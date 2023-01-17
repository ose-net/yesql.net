﻿using System;

namespace YeSql.Net;

/// <summary>
/// Represents the parser that extracts the SQL statements from the tags.
/// </summary>
public partial class YeSqlParser
{
    /// <summary>
    /// Gets the dictionary containing the SQL statements that have been parsed from the data source (e.g., a SQL file).
    /// </summary>
    internal YeSqlDictionary SqlStatements { get; } = new();

    /// <summary>
    /// Gets the result of the validation performed on the SQL statements.
    /// </summary>
    internal YeSqlValidationResult ValidationResult { get; } = new();

    /// <summary>
    /// Gets the name of the file from which the SQL statements were parsed.
    /// </summary>
    internal string SqlFileName { get; }

    /// <summary>
    /// Start the parsing to extract the SQL statements from a data source.
    /// </summary>
    /// <param name="source">The data source to parsing.</param>
    /// <returns>A collection containing the tags with their associated SQL statements.</returns>
    /// <exception cref="ArgumentNullException"><c>source</c> is <c>null</c>.</exception>
    internal IYeSqlCollection Parse(string source)
        => Parse(source, out _);

    /// <inheritdoc cref="Parse(string)" />
    /// <param name="validationResult">The validation result of the parsing process.</param>
    public IYeSqlCollection Parse(string source, out YeSqlValidationResult validationResult)
    {
        if(source is null)
            throw new ArgumentNullException(nameof(source));

        validationResult = ValidationResult;
        return SqlStatements;
    }
}
