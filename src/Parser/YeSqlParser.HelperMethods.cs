using System;
using System.Collections.Generic;
using System.Text;

namespace YeSql.Net;

/// <inheritdoc cref="YeSqlParser"/>
public partial class YeSqlParser
{
	/// <summary>
	/// Determines if a line of text is a comment line that does not contain a tag.
	/// </summary>
	/// <param name="line">The line of text to check.</param>
	/// <returns>True if the line is a comment without a tag, false otherwise.</returns>
	private bool IsCommentWithoutTag(string line)
	{

	}

	/// <summary>
	/// Determines if a line of text is a comment line that contains a tag.
	/// </summary>
	/// <param name="line">The line of text to check.</param>
	/// <returns>True if the line is a comment with a tag, false otherwise.</returns>
	private bool IsCommentWithTag(string line)
	{

	}

	/// <summary>
	/// Extracts the tag name from a line of text that is a comment with a tag.
	/// </summary>
	/// <param name="line">The line of text to extract the tag name from.</param>
	/// <returns>The name of the tag, or an empty string if no tag name could be extracted.</returns>
	private string ExtractTagName(string line)
	{

	}
}
