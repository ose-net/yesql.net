using System;

namespace YeSql.Net;

/// <summary>
/// The Exception is thrown when there is an error while parsing a SQL file using the <see cref="YeSqlParser"/>.
/// </summary>
public class YeSqlParserException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlParserException"/> class with a default error message.
    /// </summary>
    public YeSqlParserException() : base(ExceptionMessages.YeSqlParserDefaultMessage) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlParserException"/> class with a custom error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public YeSqlParserException(string message) : base(message) { }
}
