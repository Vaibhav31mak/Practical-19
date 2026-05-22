namespace Practical19.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterViewModel model);
    Task<AuthResponse> LoginAsync(LoginViewModel model, bool useCookieSignIn);
    Task SignOutAsync();
}
