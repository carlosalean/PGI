using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using PGI_AF.Interfaces;
using System.Collections.Generic;
using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class CasosService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public CasosService(HttpClient httpClient, IAuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }

        private async Task AddAuthorizationHeader()
        {
            var token = await _authService.GetToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<Caso>> GetCasosAsync()
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.GetAsync("api/casos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Caso>>();
        }

        public async Task<Caso> GetCasoAsync(int id)
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.GetAsync($"api/casos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Caso>();
        }

        public async Task<Caso> CreateCasoAsync(Caso caso)
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.PostAsJsonAsync("api/casos", caso);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Caso>();
        }

        public async Task UpdateCasoAsync(int id, Caso caso)
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.PutAsJsonAsync($"api/casos/{id}", caso);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCasoAsync(int id)
        {
            await AddAuthorizationHeader();

            var response = await _httpClient.DeleteAsync($"api/casos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
