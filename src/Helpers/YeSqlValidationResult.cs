using System.Collections.Generic;
using System.Text;
using System;
using System.Collections;

namespace YeSql.Net;

/// <summary>
/// Represents a container for the results of a validation.
/// </summary>
public class YeSqlValidationResult : IEnumerable<string>
{
    /// <summary>
    /// Allows access to the errors collection.
    /// </summary>
    private readonly List<string> _errors = new();

    /// <summary>
    /// Checks if there has been an error.
    /// </summary>
    /// <returns><c>true</c> if an error occurred, otherwise <c>false</c>.</returns>
    public bool HasError()
        => _errors.Count > 0;

    /// <summary>
    /// Gets the number of errors contained in the container.
    /// </summary>
    public int Count => _errors.Count;

    /// <summary>
    /// Gets the error messages.
    /// </summary>
    public string ErrorMessages
    {
        get
        {
            if (_errors.Count == 0)
                return string.Empty;
            var stringBuilder = new StringBuilder(Environment.NewLine);
            foreach (var error in _errors)
                stringBuilder.Append(error + Environment.NewLine);
            return stringBuilder.ToString();
        }
    }

    /// <summary>
    /// Adds the error message to the collection.
    /// </summary>
    /// <param name="errorMessage">The message that describes the error.</param>
    internal void Add(string errorMessage)
        => _errors.Add(errorMessage);

    /// <summary>
    /// Returns an enumerator that iterates through the errors contained in the container.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the errors contained in the container.</returns>
    public IEnumerator<string> GetEnumerator()
    {
        foreach (var error in _errors)
            yield return error;
    }

    /// <inheritdoc cref="GetEnumerator" />
    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
}
