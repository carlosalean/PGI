using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class CasosService
    {
        private readonly HttpClient _httpClient;

        public CasosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Caso>> GetCasosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Caso>>("api/Casos");
        }

        public async Task<Caso> GetCasoAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Caso>($"api/Casos/{id}");
        }

        public async Task<Caso> CreateCasoAsync(Caso caso)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Casos", caso);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Caso>();
        }

        public async Task UpdateCasoAsync(int id, Caso caso)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Casos/{id}", caso);   
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCasoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Casos/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
