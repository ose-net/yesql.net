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
    /// Returns a string that represents the current object of type <see cref="TagModel" />.
    /// </summary>
    /// <returns>A string that represents the current object of type <see cref="TagModel" />.</returns>
    public override string ToString()
        => $"-- name: {Name}\n{SqlStatement}\n";
}
