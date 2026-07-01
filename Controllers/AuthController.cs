using GPACARICOM.Models;
using GPACARICOM.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace GPACARICOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var response = await _auth.Login(request);

            if (response == null)
                return Unauthorized("Invalid username or password");
    var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, "1"),
    new Claim(ClaimTypes.Name, "Rahieme"),
    new Claim(ClaimTypes.Role, "1")
};
            var identity = new ClaimsIdentity(
    claims,
    CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);



            await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       principal);

            return Ok(response);
        }


    }
}
