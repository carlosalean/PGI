using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class MaquinasService
    {
        private readonly HttpClient _httpClient;

        public MaquinasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Maquina>> GetMaquinasAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Maquina>>("api/Maquinas");
        }

        public async Task<List<Maquina>> GetMaquinasWithAssetsAsync(int CasoID)
        {
            return await _httpClient.GetFromJsonAsync<List<Maquina>>($"api/Maquinas/Assets/{CasoID}");
        }

        public async Task<List<Maquina>> GetMaquinasCasoAsync(int CasoID)
        {
            return await _httpClient.GetFromJsonAsync<List<Maquina>>($"api/Maquinas/Caso/{CasoID}");
        }

        public async Task<Maquina> GetMaquinaAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Maquina>($"api/Maquinas/{id}");
        }

        public async Task<Maquina> CreateMaquinaAsync(Maquina caso)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Maquinas", caso);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Maquina>();
        }

        public async Task UpdateMaquinaAsync(int id, Maquina caso)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Maquinas/{id}", caso);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMaquinaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Maquinas/{id}");
            response.EnsureSuccessStatusCode();
        }

    }

}
