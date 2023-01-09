using System;

namespace YeSql.Net;

public class YeSqlParserException : Exception
{
    public YeSqlParserException() : base(ExceptionMessages.YeSqlParserDefaultMessage) { }

    public YeSqlParserException(string message) : base(message) { }
}
