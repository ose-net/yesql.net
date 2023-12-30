using System;
using System.Collections.Generic;
using System.Text;

namespace YeSql.Net;

/// <summary>
/// Represents the details of a SQL file.
/// </summary>
internal readonly struct SqlFile
{
    /// <summary>
    /// Gets or sets the file name of the SQL file.
    /// </summary>
    public string FileName { get; init; }

    /// <summary>
    /// Gets or sets the content of the SQL file.
    /// </summary>
    public string Content { get; init; }
}
