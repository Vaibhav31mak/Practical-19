namespace Practical19.Controllers;

/// <summary>
/// Handles account registration, authentication, and sign-out.
/// </summary>
public class AccountController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Displays the registration form.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true && !User.IsInRole("User"))
        {
            return Forbid();
        }

        return View();
    }

    /// <summary>
    /// Creates a new user account.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (User.Identity?.IsAuthenticated == true && !User.IsInRole("User"))
        {
            return Forbid();
        }
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _authService.RegisterAsync(model);
        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        return RedirectToAction("Login", "Account");
    }

    /// <summary>
    /// Displays the login form.
    /// </summary>
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Profile", "Users");
        }

        return View();
    }

    /// <summary>
    /// Signs the user in and redirects to their profile on success.
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _authService.LoginAsync(model, true);
        if (!result.Success)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

        return RedirectToAction("Profile", "Users");
    }

    /// <summary>
    /// Signs the current user out.
    /// </summary>
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    /// <summary>
    /// Shows the access denied page.
    /// </summary>
    [HttpGet]
    public IActionResult AccessDenied() => View();
}
