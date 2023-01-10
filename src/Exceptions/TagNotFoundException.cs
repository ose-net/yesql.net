using System;

namespace YeSql.Net;

/// <summary>
/// The exception that is thrown when the tag is not found in <see cref="IYeSqlCollection"/>
/// </summary>
public class TagNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TagNotFoundException"/> class with a default message.
    /// </summary>
    public TagNotFoundException() : base(ExceptionMessages.TagNotFoundDefaultMessage) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="TagNotFoundException"/> class with a custom message.
    /// </summary>
    /// <param name="message">The custom message to be displayed with the exception.</param>
    public TagNotFoundException(string message) : base(message) { }
}
