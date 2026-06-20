using GPACARICOM.Services.Interfaces;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace GPACARICOM.Services
{
    public class DatabaseConnectionService : IDatabaseConnectionService
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnection GetConnection()
        {
            // Try standard ConnectionStrings section first, fallback to simple key
            var cs = _configuration.GetConnectionString("DefaultConnection")
                     ?? _configuration["DefaultConnection"]
                     ?? string.Empty;

            return new MySqlConnection(cs);
        }
    }
}
