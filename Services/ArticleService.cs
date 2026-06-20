using GPACARICOM.Models;
using MySql.Data.MySqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace GPACARICOM.Services.Interfaces
{
    public class ArticleService : IArticleService
    {
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
            string query = "SELECT * FROM articles";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

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
                        : reader.GetString("hyperlink")
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
