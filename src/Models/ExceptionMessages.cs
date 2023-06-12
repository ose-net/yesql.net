namespace YeSql.Net;

/// <summary>
/// Represents the messages of an exception.
/// </summary>
internal class ExceptionMessages
{
    public const string FileNotFound                       = "{0}: error: No such file or directory.";
    public const string FileHasNotSqlExtension             = "error: '{0}' has no sql extension.";
    public const string ParameterIsNullOrEmptyOrWhiteSpace = "'{0}' parameter cannot have a null value, empty string or white-space characters.";
    public const string NoneFileFoundInSpecifiedDirectory  = "error: No sql file found in the directory '{0}'.";
    public const string YeSqlParserDefault                 = "error: Parser found syntax errors.";
    public const string YeSqlLoaderDefault                 = "error: Loader found an error while loading the SQL file.";
    public const string DataSourceIsEmptyOrWhitespace      = "Data source is empty or consists only in whitespace.";
    public const string TagNotFound                        = "The given tag '{0}' was not present in the collection.";
    public const string TagNotFoundDefault                 = "error: No tag found in the collection.";
    public const string DuplicateTagName                   = "The given tag '{0}' is duplicated.";
    public const string TagIsEmptyOrWhitespace             = "The tag name is empty.";
    public const string LineIsNotAssociatedWithAnyTag      = "'{0}' line is not associated with any tag.";
}
