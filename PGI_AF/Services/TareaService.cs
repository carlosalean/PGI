using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class TareasService
    {
        private readonly HttpClient _httpClient;

        public TareasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Tarea>> GetTareaCasoAsync(int idCaso)
        {
            return await _httpClient.GetFromJsonAsync<List<Tarea>>($"api/Tareas/Caso/{idCaso}");
        }

        public async Task<Tarea> GetTareAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tarea>($"api/Tareas/{id}");
        }

        public async Task<Tarea> CreateTareaAsync(Tarea tarea)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tareas", tarea);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Tarea>();
        }

        public async Task UpdateTareaAsync(int id, Tarea tarea)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Tareas/{id}", tarea);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTareaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tareas/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
