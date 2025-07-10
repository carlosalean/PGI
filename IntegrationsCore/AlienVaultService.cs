using Microsoft.Extensions.Options;

namespace IntegrationsCore
{
    public class AlienVaultOptions
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class AlienVaultService : IIOCService
    {
        private readonly string _apiKey;
        private const string AlienVaultUrl = "https://otx.alienvault.com/api/v1/indicators/file/";

        public AlienVaultService(IOptions<OpenAIOptions> options)
        {
            _apiKey = options.Value.ApiKey;
        }

        public async Task<(string resumen, string descripcion)> AnalyzeFileAsync(byte[] fileData, string fileName)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-OTX-API-KEY", _apiKey);

            var fileHash = ConvertToSha256(fileData);

            var response = await client.GetAsync($"{AlienVaultUrl}{fileHash}/general");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            // Simulación de procesamiento de resumen y descripción
            var resumen = $"Resumen de análisis para {fileName}";
            var descripcion = result;
            return (resumen, descripcion);
        }

        private string ConvertToSha256(byte[] fileData)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(fileData);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
