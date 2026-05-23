namespace Practical19.Services;

/// <summary>
/// Provides role management operations.
/// </summary>
public class RoleService(RoleManager<ApplicationRole> roleManager) : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    /// <summary>
    /// Creates a new role.
    /// </summary>
    public async Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model)
    {
        if (await _roleManager.RoleExistsAsync(model.Name))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });
        }

        return await _roleManager.CreateAsync(new ApplicationRole { Name = model.Name });
    }

    /// <summary>
    /// Gets all available role names.
    /// </summary>
    public async Task<IEnumerable<string>> GetRolesAsync()
    {
        return await _roleManager.Roles.Select(role => role.Name ?? string.Empty).ToListAsync();
    }

    /// <summary>
    /// Gets a role by name.
    /// </summary>
    public async Task<RoleNameViewModel?> GetRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        return role == null ? null : new RoleNameViewModel { Name = role.Name ?? string.Empty };
    }

    /// <summary>
    /// Updates a role name.
    /// </summary>
    public async Task<IdentityResult> UpdateRoleAsync(string roleName, RoleNameViewModel model)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
        }
        if (!string.Equals(role.Name, model.Name, StringComparison.OrdinalIgnoreCase)
            && await _roleManager.RoleExistsAsync(model.Name))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role already exists." });
        }
        role.Name = model.Name;
        return await _roleManager.UpdateAsync(role);
    }

    /// <summary>
    /// Deletes a role by name.
    /// </summary>
    public async Task<IdentityResult> DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role not found." });
        }

        return await _roleManager.DeleteAsync(role);
    }
}
