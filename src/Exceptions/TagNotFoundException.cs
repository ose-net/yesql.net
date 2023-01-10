using System;

namespace YeSql.Net;

/// <summary>
/// Exception thrown when a tag is not found in the SQL file being parsed.
/// </summary>
public class TagNotFoundException : Exception
{
    /// <summary>
    /// Creates a new instance of the TagNotFoundException class with a default message.
    /// </summary>
    public TagNotFoundException() : base(ExceptionMessages.TagNotFoundDefaultMessage) { }

    /// <summary>
    /// Creates a new instance of the TagNotFoundException class with a custom message.
    /// </summary>
    /// <param name="message">The custom message to be displayed with the exception.</param>
    public TagNotFoundException(string message) : base(message) { }
}
