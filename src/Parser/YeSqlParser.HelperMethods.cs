using System;
using System.Collections.Generic;
using System.Text;

namespace YeSql.Net;

// This class defines the private (or internal) helper methods.
public partial class YeSqlParser
{
    /// <summary>
    /// Checks if the line of text is a comment without a tag.
    /// </summary>
    /// <param name="line">The line of text to validate.</param>
    /// <returns><c>true</c> if the line is a comment without a tag, otherwise <c>false</c>.</returns>
	/// <example>
	/// -- This is a comment.
	/// </example>
    private bool IsCommentWithoutTag(string line)
	{
		throw new NotImplementedException();
	}

    /// <summary>
    /// Checks if the line of text is a comment with tag.
    /// </summary>
    /// <param name="line">The line of text to validate.</param>
    /// <returns><c>true</c> if the line is a comment with tag, otherwise <c>false</c>.</returns>
    /// <example>
	/// -- name: This is a comment with tag.
	/// </example>
    private bool IsCommentWithTag(string line)
	{
        throw new NotImplementedException();
    }

    /// <summary>
    /// Extracts the tag name from a comment.
    /// </summary>
    /// <param name="line">The line with the tag name.</param>
    /// <returns>The tag name extracted.</returns>
    private string ExtractTagName(string line)
	{
        throw new NotImplementedException();
    }
}
