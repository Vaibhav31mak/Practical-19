namespace Practical19.Models.ViewModels;

/// <summary>
/// Represents registration form input.
/// </summary>
public class RegisterViewModel
{
    /// <summary>
    /// Gets or sets the user's full name.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the registration email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the registration password.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the confirmation password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = string.Empty;
}
