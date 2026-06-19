using MySql.Data.MySqlClient;

namespace GPACARICOM.Services.Interfaces
{
    
        public interface IDatabaseConnectionService
        {
            MySqlConnection GetConnection();
        }
    
}
