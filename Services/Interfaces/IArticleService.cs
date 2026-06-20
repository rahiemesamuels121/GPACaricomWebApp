using GPACARICOM.Models;

namespace GPACARICOM.Services.Interfaces
{
    public interface IArticleService
    {
        public Task<List<Article>> GetArticlesAsync();
        Article GetArticle(int id);
       bool AddNewArticle(Article article);
       bool UpdateArticle(Article article);
       bool DeleteArticle(int id);

    }
}
