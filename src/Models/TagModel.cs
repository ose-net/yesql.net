﻿namespace YeSql.Net;

/// <summary>
/// Represents the tag with its associated SQL statement.
/// </summary>
public struct ModelTag
{
    /// <summary>
    /// Gets the name of the tag.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the SQL statement of the tag.
    /// </summary>
    public string SqlStatement { get; init; }

    /// <summary>
    /// Deconstructs the current <see cref="ModelTag" />.
    /// </summary>
    /// <param name="name">The name of the current <see cref="ModelTag" />.</param>
    /// <param name="sqlStatement">The SQL statement of the current <see cref="ModelTag" />.</param>
    public void Deconstruct(out string name, out string sqlStatement)
    {
        name = Name;
        sqlStatement = SqlStatement;
    }

    /// <summary>
    /// Returns a string that represents the current object of type <see cref="ModelTag" />.
    /// </summary>
    /// <returns>A string that represents the current object of type <see cref="ModelTag" />.</returns>
    public override string ToString()
        => $"-- name: {Name}\n{SqlStatement}\n";
}
