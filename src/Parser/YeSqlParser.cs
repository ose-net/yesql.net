using System;


namespace YeSql.Net;

/// <summary>
/// A partial class that contains methods and properties related to parsing and validating SQL statements in a YeSQL file.
/// </summary>
public partial class YeSqlParser
{
    /// <summary>
    /// A dictionary containing the SQL statements that have been parsed from the source code.
    /// </summary>
    private YeSqlDictionary _sqlStatements;

    /// <summary>
    /// The result of the validation performed on the SQL statements.
    /// </summary>
    internal YeSqlValidationResult validationResult;

    /// <summary>
    /// The name of the file from which the SQL statements were parsed.
    /// </summary>
    internal string sqlFileName;

    /// <summary>
    /// Parses an SQL file and returns a collection of SQL statements.
    /// </summary>
    /// <param name="source">The source code of the SQL file to parse.</param>
    /// <param name="result">The validation result of the parsing process.</param>
    /// <returns>A collection of SQL statements.</returns>
    public IYeSqlCollection Parse(string source, YeSqlValidationResult result)
    {

    }

}
