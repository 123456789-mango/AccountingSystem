using AccountingSystem.Application.Services;
using AccountingSystem.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AccountingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;

    public AuthController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginVM vm)
    {
        var user =
            await _userService.ValidateLoginAsync(vm);

        if (user == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        return Ok(new
        {
            user.Id,
            user.FullName,
            user.Email,
            user.Role
        });
    }
}