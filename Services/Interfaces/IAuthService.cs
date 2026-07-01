using GPACARICOM.Models;
using Microsoft.AspNetCore.Identity.Data;
using GPACARICOM.Models;

namespace GPACARICOM.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserLoginResponse?> Login(UserLoginRequest request);
    }
}
