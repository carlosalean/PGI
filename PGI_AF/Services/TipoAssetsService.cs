using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class TipoAssetsService
    {
        private readonly HttpClient _httpClient;

        public TipoAssetsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TipoAsset>> GetTipoAssetAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TipoAsset>>("api/TipoAssets");
        }

        public async Task<TipoAsset> GetTipoAssetAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TipoAsset>($"api/TipoAssets/{id}");
        }

        public async Task<TipoAsset> CreateTipoAssetAsync(TipoAsset tipoAsset)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TipoAssets", tipoAsset);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TipoAsset>();
        }

        public async Task UpdateTipoAssetAsync(int? id, TipoAsset tipoAsset)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/TipoAssets/{id}", tipoAsset);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTipoAssetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/TipoAssets/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
