using System;

namespace YeSql.Net;

/// <summary>
/// The exception is thrown when there are one or more errors during the process of loading SQL files using the <see cref="YeSqlLoader"/> class.
/// </summary>
public class YeSqlLoaderException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlLoaderException" /> class with a default error message.
    /// </summary>
	public YeSqlLoaderException() : base(ExceptionMessages.YeSqlLoaderDefault) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="YeSqlLoaderException" /> class with the a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
	public YeSqlLoaderException(string message) : base(message) { }
}
