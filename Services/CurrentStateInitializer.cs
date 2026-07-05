namespace GPACARICOM.Services
{
    using Microsoft.AspNetCore.Components.Authorization;

    public class CurrentUserInitializer
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly CurrentUserService _currentUser;

        public CurrentUserInitializer(
            AuthenticationStateProvider authenticationStateProvider,
            CurrentUserService currentUser)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _currentUser = currentUser;
        }

        public async Task InitializeAsync()
        {
            var state =
                await _authenticationStateProvider.GetAuthenticationStateAsync();

            _currentUser.User = state.User;
        }
    }
}
