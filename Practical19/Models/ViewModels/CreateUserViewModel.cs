namespace Practical19.Models.ViewModels;

public class CreateUserViewModel
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string Role { get; set; } = string.Empty;
}
