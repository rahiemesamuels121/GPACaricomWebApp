using GPACARICOM.Models;

namespace GPACARICOM.Services.Interfaces
{
    public interface IArticleService
    {
        List<Article> GetArticles();
        Article GetArticle(int id);
       bool AddNewArticle(Article article);
       bool UpdateArticle(Article article);
       bool DeleteArticle(int id);

    }
}
