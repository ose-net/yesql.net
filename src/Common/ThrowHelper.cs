namespace YeSql.Net;

/// <summary>
/// Helper methods to efficiently throw exceptions.
/// </summary>
internal class ThrowHelper
{
    /// <summary>
    /// Throws an <see cref="ArgumentNullException"/> if <c>argument</c> is <c>null</c>.
    /// </summary>
    /// <param name="argument">
    /// The reference type argument to validate as non-null.
    /// </param>
    /// <param name="paramName">
    /// The name of the parameter with which argument corresponds.
    /// </param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void ThrowIfNull(object argument, string paramName)
    {
        if (argument is null)
            throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Throws an exception if the <paramref name="argument"/> contains elements with a null value, 
    /// an empty string, or consists only of white-space characters.
    /// </summary>
    /// <param name="argument">
    /// The collection argument to validate.
    /// </param>
    /// <param name="paramName">
    /// The name of the parameter with which argument corresponds.
    /// </param>
    /// <exception cref="ArgumentException"></exception>
    public static void ThrowIfContainsNullOrWhiteSpace(IEnumerable<string> argument, string paramName)
    {
        if (argument.ContainsNullOrWhiteSpace())
        {
            var message = string.Format(ExceptionMessages.CollectionHasNullValueOrOnlyWhitespace, paramName);
            throw new ArgumentException(message);
        }
    }
}
