using System.Linq;

namespace YeSql.Net;

public static class FormattingMessage
{
    /// <summary>
    /// Formats an error message in case the parser encounters errors.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="actualValue">The actual value that caused the error.</param>
    /// <param name="lineNumber">The line number that caused the error.</param>
    /// <param name="column">The column that caused the error.</param>
    /// <param name="sqlFileName">The name of the sql file that caused the error.</param>
    /// <returns>A formatted error message.</returns>
    public static string FormatParserExceptionMessage(string message, 
                                                      object actualValue = null, 
                                                      int? lineNumber = null, 
                                                      int? column = null, 
                                                      string sqlFileName = null)
    {
        if (AreNotNull(sqlFileName, lineNumber, column, actualValue))
            return $"{sqlFileName}:(line {lineNumber}, col {column}): error: {string.Format(message, actualValue)}";

        if (AreNotNull(lineNumber, column, actualValue))
            return $"Parsing error (line {lineNumber}, col {column}): error: {string.Format(message, actualValue)}";

        if (AreNotNull(sqlFileName, lineNumber, column))
            return $"{sqlFileName}:(line {lineNumber}, col {column}): error: {message}";

        if (AreNotNull(lineNumber, column))
            return $"Parsing error (line {lineNumber}, col {column}): error: {message}";

        if (sqlFileName is not null)
            return $"{sqlFileName}: error: {message}";

        return $"Parsing error: {message}";
    }

    /// <summary>
    /// Checks if the passed elements are not null.
    /// </summary>
    /// <param name="elements">The elements to validate.</param>
    /// <returns>true if all elements are not null, or false.</returns>
    private static bool AreNotNull(params object[] elements)
        => !elements.Contains(null);
}
