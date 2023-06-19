using System;

namespace YeSql.Net;

/// <summary>
/// The exception is thrown when there are one or more errors while parsing a data source (e.g., SQL file) using the <see cref="YeSqlParser" />.
/// </summary>
public class YeSqlParserException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlParserException" /> class with a default error message.
    /// </summary>
    public YeSqlParserException() : base(ExceptionMessages.YeSqlParserDefault) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlParserException" /> class with the a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public YeSqlParserException(string message) : base(message) { }
}
