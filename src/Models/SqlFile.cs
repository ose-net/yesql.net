using System;
using System.Collections.Generic;
using System.Text;

namespace YeSql.Net;

/// <summary>
/// Represents a struct that is used to store the file name and contents of an SQL file.
/// </summary>
internal struct SqlFile
{
    /// <summary>
    /// Gets or sets the file name of the SQL file.
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the contents of the SQL file.
    /// </summary>
    public string Content { get; set; }
}
