using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using PGI_AF.Interfaces;

namespace PGI_AF.Autentication
{
    public class TokenProvider : ITokenProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly string _tokenKey;

        public TokenProvider(ILocalStorageService localStorage, IConfiguration configuration)
        {
            _localStorage = localStorage;
            _tokenKey = configuration["Jwt:TokenKey"] ?? "authToken";
        }

        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>(_tokenKey);
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetItemAsync(_tokenKey, token);
        }

        public async Task RemoveTokenAsync()
        {
            await _localStorage.RemoveItemAsync(_tokenKey);
        }
    }
}
