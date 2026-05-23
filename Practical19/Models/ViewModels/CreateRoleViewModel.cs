namespace Practical19.Models.ViewModels;

/// <summary>
/// Represents role creation input.
/// </summary>
public class CreateRoleViewModel
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}

/// <summary>
/// Represents role name editing input.
/// </summary>
public class RoleNameViewModel
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}
