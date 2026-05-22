namespace Practical19.Services.Interfaces;

public interface IUserService
{
    Task<UserProfileViewModel?> GetCurrentAsync(ClaimsPrincipal principal);
    Task<IdentityResult> UpdateCurrentAsync(ClaimsPrincipal principal, UpdateProfileViewModel model);
    Task<IdentityResult> DeleteCurrentAsync(ClaimsPrincipal principal);
    Task<IEnumerable<UserProfileViewModel>> GetAllAsync();
    Task<IdentityResult> CreateUserAsync(CreateUserViewModel model);
    Task<IdentityResult> UpdateUserAsync(string userId, UpdateProfileViewModel model);
    Task<IdentityResult> DeleteUserAsync(string userId);
}
