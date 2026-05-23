namespace Practical19.Controllers;

/// <summary>
/// Manages role CRUD operations for administrators.
/// </summary>
[Authorize(Policy = "AdminOnly")]
public class RolesController(IRoleService roleService) : Controller
{
    private readonly IRoleService _roleService = roleService;

    /// <summary>
    /// Lists available roles.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleService.GetRolesAsync();
        return View(roles);
    }

    /// <summary>
    /// Displays the create role form.
    /// </summary>
    [HttpGet]
    public IActionResult Create() => View(new CreateRoleViewModel());

    /// <summary>
    /// Creates a new role.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRoleViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _roleService.CreateRoleAsync(model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays the edit form for a role.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(string roleName)
    {
        var role = await _roleService.GetRoleAsync(roleName);
        return role == null ? NotFound() : View(role);
    }

    /// <summary>
    /// Updates a role name.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string roleName, RoleNameViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var result = await _roleService.UpdateRoleAsync(roleName, model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Displays delete confirmation for a role.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Delete(string roleName)
    {
        var role = await _roleService.GetRoleAsync(roleName);
        return role == null ? NotFound() : View(role);
    }

    /// <summary>
    /// Deletes the specified role.
    /// </summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string roleName)
    {
        var result = await _roleService.DeleteRoleAsync(roleName);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            var role = await _roleService.GetRoleAsync(roleName);
            return role == null ? NotFound() : View("Delete", role);
        }

        return RedirectToAction(nameof(Index));
    }
}
