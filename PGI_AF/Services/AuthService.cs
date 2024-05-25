using BackEnd_PGI.Model;
using Microsoft.AspNetCore.Components.Authorization;
using PGI_AF.Autentication;
using PGI_AF.Interfaces;

namespace PGI_AF.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

        public AuthService(HttpClient httpClient, ITokenProvider tokenProvider, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
            _authenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { Username = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResult>();
                if (result != null)
                {
                    await _tokenProvider.SetTokenAsync(result.Token);
                    await _authenticationStateProvider.MarkUserAsAuthenticated(result.Token);
                    return true;
                }
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _tokenProvider.RemoveTokenAsync();
            await _authenticationStateProvider.MarkUserAsLoggedOut();
        }

        public async Task<bool> RegisterAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/create", usuario);
            return response.IsSuccessStatusCode;
        }

        private class TokenResult
        {
            public string Token { get; set; }
        }
    }


}
