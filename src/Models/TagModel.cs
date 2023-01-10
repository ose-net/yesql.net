namespace YeSql.Net;

/// <summary>
/// Represents the tag with its associated SQL statement.
/// </summary>
public struct TagModel
{
    /// <summary>
    /// Gets the name of the tag.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets the SQL statement of the tag.
    /// </summary>
    public string SqlStatement { get; set; }

    /// <summary>
    /// Deconstructs the current <see cref="TagModel" />.
    /// </summary>
    /// <param name="name">The name of the current <see cref="TagModel" />.</param>
    /// <param name="sqlStatement">The SQL statement of the current <see cref="TagModel" />.</param>
    public void Deconstruct(out string name, out string sqlStatement)
    {
        name = Name;
        sqlStatement = SqlStatement;
    }

    /// <summary>
    /// Returns a string that represents the current object of type <see cref="TagModel" />.
    /// </summary>
    /// <returns>A string that represents the current object of type <see cref="TagModel" />.</returns>
    public override string ToString()
        => $"-- name: {Name}\n{SqlStatement}\n";
}
