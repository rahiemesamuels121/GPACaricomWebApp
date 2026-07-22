using GPACARICOM.Models;
using GPACARICOM.Services.API;
using GPACARICOMAPI.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
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

            return Redirect("/login");

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
        return Redirect("/dashboard");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] AppUserDTO request) { 

        var userToRegister = new AppUserDTO()
        {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Telephone = request.Telephone,
            Address = request.Address,
            Organization = request.Organization,
            RoleId = request.RoleId,
            Password = request.Password,
            Email  = request.Email,
        };

        var response = await _authApiService.Register(userToRegister);
        Console.WriteLine(response);

        if (!response)
        {
            return BadRequest();
        }
        return Redirect("/");
    }


    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme
            );
        return Redirect("/");
    }

    [Authorize]
    [HttpGet("check")]
    public IActionResult Check()
    {
        return Ok();
    }

}