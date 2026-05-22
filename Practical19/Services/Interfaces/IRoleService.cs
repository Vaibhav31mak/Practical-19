namespace Practical19.Services.Interfaces;

public interface IRoleService
{
    Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model);
    Task<IEnumerable<string>> GetRolesAsync();
}
