using System.Collections.Generic;
using System.Linq;

namespace YeSql.Net;

internal static class EnumerableExtensions
{
    /// <summary>
    /// Determines if a sequence is empty.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <c>source</c>.</typeparam>
    /// <param name="source">The <see cref="IEnumerable{T}"/> to check.</param>
    /// <returns><c>true</c> if the sequence is empty, otherwise <c>false</c>.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> source)
        => !source.Any();

    /// <summary>
    /// Determines if any string in the specified collection is null, empty, or contains only white-space characters.
    /// </summary>
    /// <param name="source">The collection of strings to check.</param>
    /// <returns>True if any string in the collection is null, empty, or contains only white-space characters; otherwise, false.</returns>
    public static bool ContainsNullOrWhiteSpace(this IEnumerable<string> source)
        => source.Where(string.IsNullOrWhiteSpace).Any();

}
