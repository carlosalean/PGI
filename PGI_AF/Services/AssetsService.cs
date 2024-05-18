using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class AssetsService
    {
        private readonly HttpClient _httpClient;

        public AssetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Asset>> GetAssetCasoAsync(int idCaso)
        {
            return await _httpClient.GetFromJsonAsync<List<Asset>>($"api/Assets/Caso/{idCaso}");
        }

        public async Task<List<Asset>> GetAssetMaquinaAsync(int idMaquina)
        {
            return await _httpClient.GetFromJsonAsync<List<Asset>>($"api/Assets/Maquina/{idMaquina}");
        }

        public async Task<Asset> GetAssetsWithIOCAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Asset>($"api/Assets/IOCs/{id}");
        }

        public async Task<Asset> GetTareAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Asset>($"api/Assets/{id}");
        }

        public async Task<Asset> CreateAssetAsync(Asset asset)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Assets", asset);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Asset>();
        }

        public async Task UpdateAssetAsync(int id, Asset asset)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Assets/{id}", asset);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAssetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Assets/{id}");
            response.EnsureSuccessStatusCode();
        }

    }

}
