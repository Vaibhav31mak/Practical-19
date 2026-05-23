namespace Practical19.Models.ViewModels;

/// <summary>
/// Represents login form input.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// Gets or sets the login email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the login password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
