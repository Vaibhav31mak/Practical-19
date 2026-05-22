namespace Practical19.Services;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<ApplicationRole> roleManager,
    IOptions<JwtOptions> jwtOptions)
    : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<AuthResponse> RegisterAsync(RegisterViewModel model)
    {
        var existingUser = await _userManager.FindByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return new AuthResponse
            {
                Success = false,
                Errors = ["User already exists."]
            };
        }
        if (!await _roleManager.RoleExistsAsync("User"))
        {
            return new AuthResponse
            {
                Success = false,
                Errors = ["User role not available."]
            };
        }
        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Email,
            FullName = model.FullName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return new AuthResponse
            {
                Success = false,
                Errors = result.Errors.Select(error => error.Description).ToArray()
            };
        }
        await _userManager.AddToRoleAsync(user, "User");
        var token = await CreateJwtTokenAsync(user);
        await _signInManager.SignInAsync(user, isPersistent: false);

        return new AuthResponse
        {
            Success = true,
            Token = token
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginViewModel model, bool useCookieSignIn)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return new AuthResponse
            {
                Success = false,
                Errors = new[] { "Invalid login attempt." }
            };
        }
        if (useCookieSignIn)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                user,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new[] { "Invalid login attempt." }
                };
            }
        }
        else
        {
            var valid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!valid)
            {
                return new AuthResponse
                {
                    Success = false,
                    Errors = new[] { "Invalid login attempt." }
                };
            }
        }
        var token = await CreateJwtTokenAsync(user);

        return new AuthResponse
        {
            Success = true,
            Token = token
        };
    }

    public Task SignOutAsync() => _signInManager.SignOutAsync();

    private async Task<string> CreateJwtTokenAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName ?? string.Empty)
        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
