namespace Practical19.Controllers;

using Microsoft.AspNetCore.Mvc.Rendering;

/// <summary>
/// Provides user management actions for admins and profile management for users.
/// </summary>
[Authorize]
public class UsersController(IUserService userService, IRoleService roleService, IAuthService authService) : Controller
{
    private readonly IUserService _userService = userService;
    private readonly IRoleService _roleService = roleService;
    private readonly IAuthService _authService = authService;

    /// <summary>
    /// Shows the list of users for administrators.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userService.GetAllAsync();
        return View(users);
    }

    /// <summary>
    /// Displays the create user form for administrators.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateRolesAsync();
        return View(new CreateUserViewModel());
    }

    /// <summary>
    /// Creates a new user with the selected role.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateRolesAsync();
            return View(model);
        }
        var result = await _userService.CreateUserAsync(model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            await PopulateRolesAsync();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the edit form for an existing user.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        ViewData["UserId"] = user.Id;
        return View(new UpdateProfileViewModel
        {
            Email = user.Email,
            FullName = user.FullName ?? string.Empty
        });
    }

    /// <summary>
    /// Updates the selected user.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string userId, UpdateProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["UserId"] = userId;
            return View(model);
        }
        var result = await _userService.UpdateUserAsync(userId, model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            ViewData["UserId"] = userId;
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the delete confirmation for a user.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> Delete(string userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        return user == null ? NotFound() : View(user);
    }

    /// <summary>
    /// Deletes the selected user.
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            var user = await _userService.GetByIdAsync(userId);
            return user == null ? NotFound() : View("Delete", user);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the current user's profile for self-management.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userService.GetCurrentAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        ViewData["Roles"] = string.Join(", ", user.Roles);
        return View(new UpdateProfileViewModel
        {
            Email = user.Email,
            FullName = user.FullName ?? string.Empty
        });
    }

    /// <summary>
    /// Updates the current user's profile information.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(UpdateProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var user = await _userService.GetCurrentAsync(User);
            ViewData["Roles"] = user == null ? string.Empty : string.Join(", ", user.Roles);
            return View(model);
        }
        var result = await _userService.UpdateCurrentAsync(User, model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            var user = await _userService.GetCurrentAsync(User);
            ViewData["Roles"] = user == null ? string.Empty : string.Join(", ", user.Roles);
            return View(model);
        }

        return RedirectToAction(nameof(Profile));
    }

    /// <summary>
    /// Deletes the current user's account.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMe()
    {
        var result = await _userService.DeleteCurrentAsync(User);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return await Profile();
        }
        await _authService.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

    /// <summary>
    /// Loads available roles for selection lists.
    /// </summary>
    private async Task PopulateRolesAsync()
    {
        var roles = await _roleService.GetRolesAsync();
        ViewBag.Roles = new SelectList(roles);
    }
}
