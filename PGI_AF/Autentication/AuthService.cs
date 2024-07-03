using BackEnd_PGI.Model;
using Blazored.LocalStorage;
using PGI_AF.Interfaces;
using System.Net.Http.Headers;

namespace PGI_AF.Autentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", new { Username = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TokenResult>();
                if (result != null)
                {
                    await _localStorage.SetItemAsync(TokenKey, result.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
                    return true;
                }
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(TokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<bool> RegisterAsync(Usuario usuario)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/create", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var token = await _localStorage.GetItemAsync<string>(TokenKey);
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return true;
            }
            return false;
        }

        public async Task<string> GetToken()
        {
            return await _localStorage.GetItemAsync<string>(TokenKey);
        }

        private class TokenResult
        {
            public string? Token { get; set; }
        }
    }
}
