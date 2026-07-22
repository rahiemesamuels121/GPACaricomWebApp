using GPACARICOM.Models;
using GPACARICOMAPI.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace GPACARICOM.Services.API
{
    public class AuthApiService
    {
        private readonly ApiClient _apiClient;

        public AuthApiService(ApiClient apiClient)
        {
            _apiClient = apiClient;

        }

        public async Task<UserLoginResponse?> Login(UserLoginRequest user)
        {
            var response = await _apiClient.PostAsync("Auth/login",user);
            ApiResponse<UserLoginResponse> apiRespones = await response.Content.ReadFromJsonAsync<ApiResponse<UserLoginResponse>>() ?? new  ApiResponse<UserLoginResponse>();
            Console.WriteLine($"satuscode; {response.StatusCode}");

            if (!apiRespones.success)
            {
                return null;
            }
           
            return apiRespones.data;
                
        }

        public async Task<bool> Register(AppUserDTO user)
        {
            var response = await _apiClient.PostAsync("Auth/register", user);
            ApiResponse<UserLoginResponse> apiRespones = await response.Content.ReadFromJsonAsync<ApiResponse<UserLoginResponse>>() ?? new ApiResponse<UserLoginResponse>();

            if (!apiRespones.success)
            {
                Console.WriteLine($"Status: {apiRespones.message}");
                return false;
            }

            return apiRespones.success;

        }


    }
}
