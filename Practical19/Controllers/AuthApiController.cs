namespace Practical19.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthApiController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (User.Identity?.IsAuthenticated == true && !User.IsInRole("User"))
        {
            return Forbid();
        }
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        var result = await _authService.RegisterAsync(model);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }
        var result = await _authService.LoginAsync(model, false);
        return result.Success ? Ok(result) : Unauthorized(result);
    }
}
