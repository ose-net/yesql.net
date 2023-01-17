using System.Collections.Generic;
using System.Linq;

namespace YeSql.Net;

internal static class EnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> values)
        => !values.Any();
}
