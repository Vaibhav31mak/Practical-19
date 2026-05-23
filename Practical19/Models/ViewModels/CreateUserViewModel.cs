namespace Practical19.Models.ViewModels;

/// <summary>
/// Represents admin user creation input.
/// </summary>
public class CreateUserViewModel
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
    /// <summary>
    /// Gets or sets the initial password.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the role to assign.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Role { get; set; } = string.Empty;
}
