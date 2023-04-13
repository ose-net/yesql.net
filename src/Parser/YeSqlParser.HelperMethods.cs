using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Text;
using static YeSql.Net.FormattingMessage;

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
    private bool IsCommentWithoutTag(ref Line line)
        => Regex.IsMatch(line.Text, @"^\s*--");

    /// <summary>
    /// Checks if the line of text is a comment with tag.
    /// </summary>
    /// <param name="line">The line of text to validate.</param>
    /// <returns><c>true</c> if the line is a comment with tag, otherwise <c>false</c>.</returns>
    /// <example>
	/// -- name: This is a comment with tag.
	/// </example>
    private bool IsCommentWithTag(ref Line line)
        => Regex.IsMatch(line.Text, @"^\s*--\s*name:");

    /// <summary>
    /// Extracts the tag name from a comment.
    /// </summary>
    /// <param name="line">The line with the tag name.</param>
    /// <returns>The tag name extracted; otherwise, <c>null</c> if the tag is empty.</returns>
    private string ExtractTagName(ref Line line)
    {
        var extractedTag = line.Text.Split(new[] { ':' }, MaxCount)[1];
        if(string.IsNullOrWhiteSpace(extractedTag))
        {
            ValidationResult.Add(errorMessage: FormatParserExceptionMessage(
                ExceptionMessages.TagIsEmptyOrWhitespaceMessage,
                actualValue: line.Text,
                lineNumber: line.Number,
                column: line.Text.IndexOf(NamePrefix) + 6,
                sqlFileName: _sqlFileName
            ));
            return default;
        }
        return extractedTag.Trim();
    }

    /// <summary>
    /// Checks if the tag name is duplicated.
    /// </summary>
    /// <param name="tagName">The tag name to validate.</param>
    /// <param name="line">The current line.</param>
    private void CheckIfTagIsDuplicated(string tagName, ref Line line)
    {
        if (tagName is not null)
        {
            bool isDuplicated = SqlStatements.TryAdd(tagName, string.Empty) is false;
            if (isDuplicated)
            {
                ValidationResult.Add(errorMessage: FormatParserExceptionMessage(
                    ExceptionMessages.DuplicateTagNameMessage,
                    actualValue: line.Text,
                    lineNumber: line.Number,
                    column: line.Text.IndexOf(NamePrefix) + 6,
                    sqlFileName: _sqlFileName
                ));
            }
        }
    }
}