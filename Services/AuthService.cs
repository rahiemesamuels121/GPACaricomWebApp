using GPACARICOM.Models;
using GPACARICOM.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPACARICOM.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDatabaseConnectionService _dbconn;
        private readonly IConfiguration _configuration;


        public AuthService(IDatabaseConnectionService dbconn, IConfiguration configuration)
        {
            _dbconn = dbconn;
            _configuration = configuration;
        }
        public async Task<UserLoginResponse?> Login(UserLoginRequest request)
        {
            using var connection = _dbconn.GetConnection();

            await connection.OpenAsync();

            string sql =
            @"SELECT *
      FROM users
      WHERE user_email=@user_email
      AND is_active=1";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@user_email", request.Username);

            using var reader = await cmd.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            string storedHash = reader.GetString("password_hash");

            bool valid =
                true;

            if (!valid)
                return null;

            //Generate JWT here


            return new UserLoginResponse
            {
                UserId = reader.GetInt32("user_id"),
                FirstName = reader.GetString("user_firstname"),
                Role = reader.GetInt32("user_role_id").ToString(),
                Email = reader.GetString("user_email"),
                LastName = reader.GetString("user_lastname")
            };
        }


        private string GenerateJwt(int id,
                           string username,
                           string role)
        {

            string? jwtKey = _configuration.GetValue<string>("Jwt:Key")
                   ?? _configuration["Jwt:Key"];

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT Signing Key is missing from the application configuration.");
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds =
                new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub,id.ToString()),
        new Claim(ClaimTypes.Name,username),
        new Claim(ClaimTypes.Role,role)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
