namespace Practical19.Services.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Registers a new user.
    /// </summary>
    Task<AuthResponse> RegisterAsync(RegisterViewModel model);
    /// <summary>
    /// Authenticates a user and optionally signs in with cookies.
    /// </summary>
    Task<AuthResponse> LoginAsync(LoginViewModel model, bool useCookieSignIn);
    /// <summary>
    /// Signs the current user out.
    /// </summary>
    Task SignOutAsync();
}
