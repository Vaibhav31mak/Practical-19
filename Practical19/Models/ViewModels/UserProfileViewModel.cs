namespace Practical19.Models.ViewModels;

/// <summary>
/// Represents user profile details with assigned roles.
/// </summary>
public class UserProfileViewModel
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    public string? FullName { get; set; }
    /// <summary>
    /// Gets or sets the assigned role names.
    /// </summary>
    public IReadOnlyCollection<string> Roles { get; set; } = Array.Empty<string>();
}
