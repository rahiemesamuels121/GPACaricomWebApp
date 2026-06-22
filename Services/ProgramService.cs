using GPACARICOM.Models;
using GPACARICOM.Services.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;

namespace GPACARICOM.Services
{
    public class ProgramService : IProgramService
    {
        private readonly IDatabaseConnectionService _dbconn;
        public ProgramService(IDatabaseConnectionService DBConnection)
        {
            _dbconn = DBConnection;
        }
        public Task<bool> AddNewProgram(ProgramModel program)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProgram(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProgramModel> GetProgram(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProgramModel>> GetProgramsAsync()
        {
            var programs = new List<ProgramModel>();

            var connection = _dbconn.GetConnection();
            await connection.OpenAsync();
            string query = "SELECT * \r\nFROM gpa_programs\r\nORDER BY created_date;";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                programs.Add(new ProgramModel
                {
                    programid = reader.GetInt32("program_id"),
                    programName = reader.GetString("program_name"),
                    programDescription = reader.GetString("program_description"),
                    programDate = reader.GetString("program_date"),
                    CreatedDate = reader.GetDateTime("created_date"),
                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("created_by"))
                    ? string.Empty
                    : reader.GetString("created_by"),

                    UpdatedDate = reader.IsDBNull(reader.GetOrdinal("updated_date"))
                    ? null
                    : reader.GetDateTime("updated_date"),

                    UpdatedBy = reader.IsDBNull(reader.GetOrdinal("updated_by"))
                    ? null
                    : reader.GetString("updated_by"),

                    IsDeleted = reader.GetBoolean("is_deleted"),

                    DeletedDate = reader.IsDBNull(reader.GetOrdinal("deleted_date"))
                    ? null
                    : reader.GetDateTime("deleted_date"),

                    DeletedBy = reader.IsDBNull(reader.GetOrdinal("deleted_by"))
                    ? null
                    : reader.GetString("deleted_by")

                });
            }
            return programs;

        }

        public Task<bool> UpdateProgram(ProgramModel article)
        {
            throw new NotImplementedException();
        }
    }
}
