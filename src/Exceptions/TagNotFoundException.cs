using System;

namespace YeSql.Net
{
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException() : base(ExceptionMessages.TagNotFoundMessage) { }

        public TagNotFoundException(string message) : base(message) { }
    }
}
