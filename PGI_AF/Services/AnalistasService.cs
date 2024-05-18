using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class AnalistasService
    {
        private readonly HttpClient _httpClient;

        public AnalistasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Analista>> GetAnalistaAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Analista>>("api/Analistas");
        }

        public async Task<Analista> GetAnalistaAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Analista>($"api/Analistas/{id}");
        }

        public async Task<Analista> CreateAnalistaAsync(Analista analista)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Analistas", analista);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Analista>();
        }

        public async Task UpdateAnalistaAsync(int id, Analista analista)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Analistas/{id}", analista);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAnalistaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Analistas/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
