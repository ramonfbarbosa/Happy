using Microsoft.AspNetCore.Mvc;
using OrfanatoAPI.Requests;
using OrfanatoAPI.Response;
using OrfanatoAPI.Services.Auth;

namespace OrfanatoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    public IConfiguration Configuration { get; }
    public ITokenGenerator TokenGenerator { get; set; }

    public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
    {
        Configuration = configuration;
        TokenGenerator = tokenGenerator;
    }

    [HttpPost("sign-in")]
    public IActionResult SignIn([FromBody] SignInRequest request)
    {
        try
        {
            var tokenLogin = Configuration["Jwt:Email"];
            var tokenPassword = Configuration["Jwt:Password"];
            if (request.Email == tokenLogin &&
                request.Password == tokenPassword)
            {
                string token = TokenGenerator.GenerateToken();
                return Ok(new SignInResponse
                {
                    Message = "Usuário autenticado com sucesso!",
                    Success = true,
                    Token = token,
                    TokenExpires = DateTime.Now.AddHours(int.Parse(Configuration["Jwt:HoursToExpire"]))
                });
            }
            else
            {
                string errorMessage = $"Usuario ou senha incorretos";
                return Unauthorized();
            }
        }
        catch (Exception e)
        {
            string errorMessage = $"SignInMethod error - {e.Message}";
            return StatusCode(500, errorMessage);
        }
    }
}
