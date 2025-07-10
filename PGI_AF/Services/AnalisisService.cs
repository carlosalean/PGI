using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BackEnd_PGI.Model;

namespace PGI_AF.Services
{
    public class AnalisisService
    {
        private readonly HttpClient _httpClient;

        public AnalisisService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IOC>> AnalyzeCaseAsync(int casoId)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/IOCs/analyze/case/{casoId}", casoId);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<IOC>>() ?? new List<IOC>();
            }
            else
            {
                throw new HttpRequestException("Error al analizar los assets del caso.");
            }
        }
    }
}
