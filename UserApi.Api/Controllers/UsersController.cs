using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Application.Commands.CreateUser;

namespace UserApi.Api.Controllers;



[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly CreateUserCommandHandler _handler;

    public UsersController(CreateUserCommandHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        await _handler.HandleAsync(command);
        return Ok(new { message = "User registered successfully" });
    }
}
