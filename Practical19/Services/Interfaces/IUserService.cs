namespace Practical19.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Gets the current user's profile.
    /// </summary>
    Task<UserProfileViewModel?> GetCurrentAsync(ClaimsPrincipal principal);
    /// <summary>
    /// Updates the current user's profile.
    /// </summary>
    Task<IdentityResult> UpdateCurrentAsync(ClaimsPrincipal principal, UpdateProfileViewModel model);
    /// <summary>
    /// Deletes the current user.
    /// </summary>
    Task<IdentityResult> DeleteCurrentAsync(ClaimsPrincipal principal);
    /// <summary>
    /// Gets all users with their roles.
    /// </summary>
    Task<IEnumerable<UserProfileViewModel>> GetAllAsync();
    /// <summary>
    /// Gets a user profile by id.
    /// </summary>
    Task<UserProfileViewModel?> GetByIdAsync(string userId);
    /// <summary>
    /// Creates a user and assigns a role.
    /// </summary>
    Task<IdentityResult> CreateUserAsync(CreateUserViewModel model);
    /// <summary>
    /// Updates an existing user's profile.
    /// </summary>
    Task<IdentityResult> UpdateUserAsync(string userId, UpdateProfileViewModel model);
    /// <summary>
    /// Deletes a user by id.
    /// </summary>
    Task<IdentityResult> DeleteUserAsync(string userId);
}
