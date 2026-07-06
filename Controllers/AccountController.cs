using GPACARICOM.Models;
using GPACARICOM.Services.API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GPACARICOM.Controllers;

[ApiController]
[Route("account")]
public class AccountController : Controller
{
    private readonly AuthApiService _authApiService;

    public AccountController(AuthApiService authApiService)
    {
        _authApiService = authApiService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] UserLoginRequest request)
    {
        var result = await _authApiService.Login(request);

        if (result == null || string.IsNullOrWhiteSpace(result.jwt))
            return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
            new Claim(ClaimTypes.Name, $"{result.FirstName} {result.LastName}"),
            new Claim(ClaimTypes.Role, "2"),
           

        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties()
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15)
        };

        authProperties.StoreTokens(new[]
       {
            new AuthenticationToken { Name = "api_token", Value = result.jwt }
        });


        await HttpContext.SignInAsync(
    CookieAuthenticationDefaults.AuthenticationScheme,
    principal,
    authProperties);

        Console.WriteLine(result.jwt);
        return Redirect("/");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok();
    }

    [Authorize]
    [HttpGet("check")]
    public IActionResult Check()
    {
        return Ok();
    }

}