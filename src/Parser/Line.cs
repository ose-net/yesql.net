namespace YeSql.Net;

/// <summary>
/// Represents a line of text from a file.
/// </summary>
internal readonly ref struct Line
{
    /// <summary>
    /// Gets the number/position of the line.
    /// </summary>
    public int Number { get; init; }

    /// <summary>
    /// Gets the content of the line.
    /// </summary>
    public string Text { get; init; } = string.Empty;

    public Line() { }
}
