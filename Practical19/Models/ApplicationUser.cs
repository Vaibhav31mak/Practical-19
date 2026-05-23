namespace Practical19.Models;

/// <summary>
/// Represents an application user with additional profile data.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    [StringLength(100)]
    public string? FullName { get; set; }
}
