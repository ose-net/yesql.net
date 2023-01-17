namespace YeSql.Net;

/// <summary>
/// Represents the messages of an exception.
/// </summary>
public class ExceptionMessages
{
    public const string FileNotFoundMessage                       = "{0}: error: No such file or directory.";
    public const string NoneFileFoundInSpecifiedDirectoryMessage  = "error: No sql file found in the directory '{0}'.";
    public const string YeSqlParserDefaultMessage                 = "error: Parser found syntax errors.";
    public const string YeSqlLoaderDefaultMessage                 = "error: Loader found an error while loading the SQL file.";
    public const string DataSourceIsEmptyOrWhitespaceMessage      = "Data source is empty or consists only in whitespace.";
    public const string TagNotFoundMessage                        = "The given tag '{0}' was not present in the collection.";
    public const string TagNotFoundDefaultMessage                 = "error: No tag found in the collection.";
    public const string DuplicateTagNameMessage                   = "The given tag '{0}' is duplicated.";
    public const string TagIsEmptyOrWhitespaceMessage             = "The tag name is empty.";
    public const string StatementIsNotAssociatedWithAnyTagMessage = "'{0}' statement is not associated with any tag.";
}
