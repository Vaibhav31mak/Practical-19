namespace Practical19.Models.ViewModels;

public class CreateRoleViewModel
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
}
