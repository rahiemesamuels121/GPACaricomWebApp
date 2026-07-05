using GPACARICOM.Models;

namespace GPACARICOM.Services.API
{
    public class ArticleApiService
    {
        private readonly ApiClient _apiClient;

        public  ArticleApiService(ApiClient apiClient) {
            _apiClient = apiClient;
        
        }

        public async Task<ApiResponse<List<Article>>> GetAllArticles() {
            var response = await _apiClient.GetAsync("Article/getAllArticles");
            return await response.Content.ReadFromJsonAsync<ApiResponse<List<Article>>>() ?? new ApiResponse<List<Article>>();
        }


    }
}
