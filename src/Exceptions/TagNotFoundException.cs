using System;

namespace YeSql.Net;

public class TagNotFoundException : Exception
{
    public TagNotFoundException() : base(ExceptionMessages.TagNotFoundDefaultMessage) { }

    public TagNotFoundException(string message) : base(message) { }
}
