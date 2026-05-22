namespace Practical19.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var user = await _userService.GetCurrentAsync(User);
        return user == null ? NotFound() : Ok(user);
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe(UpdateProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _userService.UpdateCurrentAsync(User, model);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors.Select(error => error.Description));
    }

    [Authorize]
    [HttpDelete("me")]
    public async Task<IActionResult> DeleteMe()
    {
        var result = await _userService.DeleteCurrentAsync(User);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors.Select(error => error.Description));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _userService.CreateUserAsync(model);
        return result.Succeeded ? Ok() : BadRequest(result.Errors.Select(error => error.Description));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{userId}")]
    public async Task<IActionResult> Update(string userId, UpdateProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await _userService.UpdateUserAsync(userId, model);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors.Select(error => error.Description));
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        var result = await _userService.DeleteUserAsync(userId);
        return result.Succeeded ? NoContent() : BadRequest(result.Errors.Select(error => error.Description));
    }
}
