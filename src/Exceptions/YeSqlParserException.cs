using System;

namespace YeSql.Net.Exceptions;

public class YeSqlParserException : Exception
{
    public YeSqlParserException() : base(ExceptionMessages.YeSqlParserDefaultMessage) { }

    public YeSqlParserException(string message) : base(message) { }
}
