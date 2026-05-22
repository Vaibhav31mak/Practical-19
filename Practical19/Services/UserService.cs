namespace Practical19.Services;

public class UserService(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
    : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

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

    public async Task<IdentityResult> DeleteCurrentAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        return await _userManager.DeleteAsync(user);
    }

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
