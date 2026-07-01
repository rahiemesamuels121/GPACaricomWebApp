namespace GPACARICOM.Services
{
    using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

    public class TokenService
    {
        private readonly ProtectedLocalStorage _storage;

        public TokenService(ProtectedLocalStorage storage)
        {
            _storage = storage;
        }

        public async Task SaveToken(string token)
        {
            await _storage.SetAsync("jwt", token);
        }

        public async Task<string?> GetToken()
        {
            var result = await _storage.GetAsync<string>("jwt");
            return result.Success ? result.Value : null;
        }

        public async Task RemoveToken()
        {
            await _storage.DeleteAsync("jwt");
        }
    }
}
