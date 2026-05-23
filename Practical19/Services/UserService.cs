namespace Practical19.Services;

/// <summary>
/// Provides user profile and administration operations.
/// </summary>
public class UserService(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
    : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    /// <summary>
    /// Gets the current user's profile details.
    /// </summary>
    public async Task<UserProfileViewModel?> GetCurrentAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
        {
            return null;
        }
        var roles = await _userManager.GetRolesAsync(user);
        return new UserProfileViewModel
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles.ToArray()
        };
    }

    /// <summary>
    /// Updates the current user's profile information.
    /// </summary>
    public async Task<IdentityResult> UpdateCurrentAsync(ClaimsPrincipal principal, UpdateProfileViewModel model)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
        user.Email = model.Email;
        user.UserName = model.Email;
        user.FullName = model.FullName;

        return await _userManager.UpdateAsync(user);
    }

    /// <summary>
    /// Deletes the current user.
    /// </summary>
    public async Task<IdentityResult> DeleteCurrentAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        return await _userManager.DeleteAsync(user);
    }

    /// <summary>
    /// Gets all users with their roles.
    /// </summary>
    public async Task<IEnumerable<UserProfileViewModel>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var results = new List<UserProfileViewModel>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            results.Add(new UserProfileViewModel
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FullName = user.FullName,
                Roles = roles.ToArray()
            });
        }

        return results;
    }

    /// <summary>
    /// Gets a user's profile by id.
    /// </summary>
    public async Task<UserProfileViewModel?> GetByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }
        var roles = await _userManager.GetRolesAsync(user);
        return new UserProfileViewModel
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Roles = roles.ToArray()
        };
    }

    /// <summary>
    /// Creates a new user and assigns a role.
    /// </summary>
    public async Task<IdentityResult> CreateUserAsync(CreateUserViewModel model)
    {
        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            return IdentityResult.Failed(new IdentityError { Description = "Role does not exist." });
        }
        var existing = await _userManager.FindByEmailAsync(model.Email);
        if (existing != null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User already exists." });
        }
        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
            FullName = model.FullName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        return result;
    }

    /// <summary>
    /// Updates an existing user's profile data.
    /// </summary>
    public async Task<IdentityResult> UpdateUserAsync(string userId, UpdateProfileViewModel model)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }
        user.Email = model.Email;
        user.UserName = model.Email;
        user.FullName = model.FullName;

        return await _userManager.UpdateAsync(user);
    }

    /// <summary>
    /// Deletes the specified user.
    /// </summary>
    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        return await _userManager.DeleteAsync(user);
    }
}
