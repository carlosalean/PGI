using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class TipoIOCsService
    {
        private readonly HttpClient _httpClient;

        public TipoIOCsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoIOC>> GetTipoIOCAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TipoIOC>>("api/TipoIOCs");
        }

        public async Task<TipoIOC> GetTipoIOCAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TipoIOC>($"api/TipoIOCs/{id}");
        }

        public async Task<TipoIOC> CreateTipoIOCAsync(TipoIOC tipoIOC)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TipoIOCs", tipoIOC);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TipoIOC>();
        }

        public async Task UpdateTipoIOCAsync(int id, TipoIOC tipoIOC)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/TipoIOCs/{id}", tipoIOC);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTipoIOCAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/TipoIOCs/{id}");
            response.EnsureSuccessStatusCode();
        }

    }

}
