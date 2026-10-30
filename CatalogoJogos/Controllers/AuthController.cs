using CatalogoJogos.Models;
using CatalogoJogos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoJogos.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    // POST /api/auth/login (rota aberta)
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        if (model.Username == "Mauricio" && model.Password == "123456")
        {
            var token = _tokenService.GenerateToken(model.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }
}
