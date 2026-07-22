using Microsoft.AspNetCore.Authentication;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiClient(HttpClient httpClient,
                    IHttpContextAccessor httpContextAccessor
        )
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    private async Task AddToken()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return;

        var token = await context.GetTokenAsync("api_token");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", token);
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        await AddToken();
        return await _httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string url, T model)
    {
        await AddToken();
        return await _httpClient.PostAsJsonAsync(url, model);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var response = await _httpClient.GetAsync("account/check");

        return response.IsSuccessStatusCode;
    }
}