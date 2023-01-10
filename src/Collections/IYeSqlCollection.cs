using System;
using System.Collections.Generic;

namespace YeSql.Net;

/// <summary>
/// Represents a collection of tags and SQL statements.
/// </summary>
public interface IYeSqlCollection : IEnumerable<TagModel>
{
    /// <summary>
    /// Gets the SQL statement associated with the specified tag.
    /// </summary>
    /// <param name="tagName">The tag of the SQL statement to get.</param>
    /// <value>
    /// The SQL statement associated with the specified tag.
    /// If the specified tag is not found, throws a <see cref="TagNotFoundException" />.
    /// </value>
    /// <exception cref="ArgumentNullException">
    /// <c>tagName</c> is <c>null</c>.
    /// </exception>
    /// <exception cref="TagNotFoundException">
    /// <c>tagName</c> does not exist in the collection.
    /// </exception>
    string this[string tagName] { get; }

    /// <summary>
    /// Gets the SQL statement associated with the specified tag.
    /// </summary>
    /// <param name="tagName">The tag of the SQL statement to get.</param>
    /// <param name="sqlStatement">
    /// When this method returns, contains the SQL statement associated with the specified tag, if the tag is found; 
    /// otherwise, the default value for the type of the <c>sqlStatement</c> parameter. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// <c>true</c> if the <see cref="IYeSqlCollection" /> contains a SQL statement with the specified tag; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <c>tagName</c> is <c>null</c>.
    /// </exception>
    bool TryGetStatement(string tagName, out string sqlStatement);
}
