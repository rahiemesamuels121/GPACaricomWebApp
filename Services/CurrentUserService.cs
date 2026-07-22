using Microsoft.AspNetCore.Components.Authorization;

namespace GPACARICOM.Services
{
    using System.Security.Claims;

    public class CurrentUserService
    {
        public ClaimsPrincipal? User { get; set; }

        public string? Jwt =>
            User?.FindFirst("access_token")?.Value;
    }
}


