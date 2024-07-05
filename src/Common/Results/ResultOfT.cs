namespace YeSql.Net;

/// <summary>
/// Represents the result of an operation.
/// </summary>
/// <typeparam name="T">A value associated to the result.</typeparam>
internal readonly ref struct Result<T>
{
    /// <summary>
    /// Gets the value associated with the result.
    /// </summary>
    public T Value { get; init; } = default;

    /// <summary>
    /// A value indicating that the result was successful.
    /// </summary>
    public bool IsSuccess { get; init; } = true;

    /// <summary>
    /// A value that indicates that the result was a failure.
    /// </summary>
    public bool IsFailed => !IsSuccess;

    public Result() { }

    /// <summary>
    /// Represents a successful operation and accepts a values as the result of the operation.
    /// </summary>
    /// <param name="value">The value to be set.</param>
    public static Result<T> Success(T value) => new()
    {
        Value = value
    };

    /// <summary>
    /// Represents an error that occurred during the execution of a operation.
    /// </summary>
    public static Result<T> Failure() => new()
    {
        IsSuccess = false
    };
}
