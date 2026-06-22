using GPACARICOM.Models;
using MySql.Data.MySqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace GPACARICOM.Services.Interfaces
{
    public class ArticleService : IArticleService
    {

        public ArticleService(IDatabaseConnectionService dbconn)
        {
            _dbconn = dbconn;
        }
        private readonly IDatabaseConnectionService _dbconn;
        public bool AddNewArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArticle(int id)
        {
            throw new NotImplementedException();
        }

        public Article GetArticle(int id)
        {
            
            throw new NotImplementedException();


        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            var articles = new List<Article>();

            var connection =  _dbconn.GetConnection();
            await connection.OpenAsync();
            string query = "SELECT * \r\nFROM articles\r\nORDER BY created_date\r\nLIMIT 3;";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            var count = 0;

            while (await reader.ReadAsync())
            {
                articles.Add(new Article
                {
                    articleId = reader.GetInt32("article_id"),
                    name = reader.GetString("name"),
                    title = reader.GetString("title"),

                    shortDescription = reader.IsDBNull(reader.GetOrdinal("short_description"))
        ? string.Empty
        : reader.GetString("short_description"),

                    fullDescription = reader.IsDBNull(reader.GetOrdinal("full_description"))
        ? string.Empty
        : reader.GetString("full_description"),

                    hyperlink = reader.IsDBNull(reader.GetOrdinal("hyperlink"))
        ? string.Empty
        : reader.GetString("hyperlink"),

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
             return articles;
        }

        public bool UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
