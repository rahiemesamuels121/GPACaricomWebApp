using GPACARICOM.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace GPACARICOM.Services.Interfaces
{
    public class TestimonialService : ItestimonialService
    {
        private readonly IDatabaseConnectionService _dbconn;
       public TestimonialService(IDatabaseConnectionService DBConnection) 
            {
                _dbconn = DBConnection;
            }
        public bool AddNewTestimonial(Article article)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTestimonial(int id)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int id)
        {
            throw new NotImplementedException();
        }

        //GETS ALL THE ARTICLES And Returns The list
        public async Task<List<TestimonialModel>> GetTestimonialsAsync()
        {
            var testimonials = new List<TestimonialModel>();

            var connection = _dbconn.GetConnection();
            await connection.OpenAsync();
            string query = "SELECT * \r\nFROM testimonials\r\nORDER BY created_date\r\nLIMIT 3;";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
           

            while (await reader.ReadAsync()) {
                testimonials.Add(new TestimonialModel
                {
                    testimonialid = reader.GetInt32("testimonial_id"),
                    testimonialUserid = reader.GetInt32("testimonial_user_id"),
                    testimonialTitle = reader.GetString("testimonial_title"),
                    testimonialbody = reader.GetString("testimonial_body"),
                    username = reader.GetString("username"),
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
            return testimonials;
        }

        public bool UpDateTestimonial(Article article)
        {
            throw new NotImplementedException();
          
        }
    }
}
