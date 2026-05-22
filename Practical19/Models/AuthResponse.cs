namespace Practical19.Models;

public class AuthResponse
{
    public bool Success { get; init; }
    public string? Token { get; init; }
    public IReadOnlyCollection<string> Errors { get; init; } = Array.Empty<string>();
}
