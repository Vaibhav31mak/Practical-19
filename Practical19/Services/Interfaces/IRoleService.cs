namespace Practical19.Services.Interfaces;

public interface IRoleService
{
    /// <summary>
    /// Creates a new role.
    /// </summary>
    Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model);
    /// <summary>
    /// Gets all role names.
    /// </summary>
    Task<IEnumerable<string>> GetRolesAsync();
    /// <summary>
    /// Gets a role by name.
    /// </summary>
    Task<RoleNameViewModel?> GetRoleAsync(string roleName);
    /// <summary>
    /// Updates a role's name.
    /// </summary>
    Task<IdentityResult> UpdateRoleAsync(string roleName, RoleNameViewModel model);
    /// <summary>
    /// Deletes a role by name.
    /// </summary>
    Task<IdentityResult> DeleteRoleAsync(string roleName);
}
