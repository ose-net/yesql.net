using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace YeSql.Net;

/// <inheritdoc cref="IYeSqlCollection" />
internal class YeSqlDictionary : IYeSqlCollection
{
    /// <summary>
    /// A dictionary containing the SQL statements that have been parsed from the data source (e.g., a SQL file).
    /// </summary>
    private readonly Dictionary<string, string> _sqlStatements = new();

    /// <inheritdoc />
    public string this[string tagName]
    {
        get
        {
            if(tagName is null)
                throw new ArgumentNullException(nameof(tagName));

            if(_sqlStatements.TryGetValue(tagName, out var sqlStatement))
                return sqlStatement;

            throw new TagNotFoundException(string.Format(ExceptionMessages.TagNotFoundMessage, tagName));
        }
    }

    /// <inheritdoc />
    public bool TryGetStatement(string tagName, out string sqlStatement)
    {
        if (tagName is null)
            throw new ArgumentNullException(nameof(tagName));

        return _sqlStatements.TryGetValue(tagName, out sqlStatement);
    }

    /// <summary>
    /// Adds the specified tag and SQL statement to the dictionary.
    /// </summary>
    /// <param name="tagName">The tag to add.</param>
    /// <param name="sqlStatement">The SQL statement to add.</param>
    internal void Add(string tagName, string sqlStatement)
        => _sqlStatements.Add(tagName, sqlStatement);

    /// <summary>
    /// Returns an enumerator that iterates through the SQL statements contained in the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the SQL statements contained in the collection.</returns>
    public IEnumerator<TagModel> GetEnumerator()
    {
        foreach(var keyValuePair in _sqlStatements)
            yield return new TagModel { Name = keyValuePair.Key, SqlStatement = keyValuePair.Value };
    }

    /// <inheritdoc cref="GetEnumerator" />
    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    /// <summary>
    /// Converts the <see cref="IYeSqlCollection" /> instance to a <see cref="string" /> object.
    /// </summary>
    /// <returns>A string that represents the current object of type <see cref="IYeSqlCollection" />.</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var tagModel in this)
            sb.Append(tagModel.ToString());
        return sb.ToString();
    }
}
