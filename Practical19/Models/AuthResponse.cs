namespace Practical19.Models;

/// <summary>
/// Represents the result of an authentication operation.
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Gets whether the authentication was successful.
    /// </summary>
    public bool Success { get; init; }
    /// <summary>
    /// Gets the JWT token when authentication succeeds.
    /// </summary>
    public string? Token { get; init; }
    /// <summary>
    /// Gets the error messages when authentication fails.
    /// </summary>
    public IReadOnlyCollection<string> Errors { get; init; } = Array.Empty<string>();
}
