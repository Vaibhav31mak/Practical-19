namespace Practical19.Models.ViewModels;

/// <summary>
/// Represents profile update input.
/// </summary>
public class UpdateProfileViewModel
{
    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
