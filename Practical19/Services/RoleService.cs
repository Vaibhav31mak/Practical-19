namespace Practical19.Services;

public class RoleService(RoleManager<ApplicationRole> roleManager) : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model)
    {
        if (await _roleManager.RoleExistsAsync(model.Name))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });
        }

        return await _roleManager.CreateAsync(new ApplicationRole { Name = model.Name });
    }

    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        return await _roleManager.Roles.Select(role => role.Name ?? string.Empty).ToListAsync();
    }
}
