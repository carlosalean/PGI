using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace IntegrationsCore
{
    public class OpenAIOptions
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class OpenAIService : IIOCService
    {
        private readonly string _apiKey;
        private const string OpenAIUrl = "https://api.openai.com/v1/completions";

        public OpenAIService(IOptions<OpenAIOptions> options)
        {
            _apiKey = options.Value.ApiKey;
        }

        public async Task<(string resumen, string descripcion)> AnalyzeFileAsync(byte[] fileData, string fileName)
        {
            // Convierte el archivo en texto para simular su análisis
            var fileContent = Encoding.UTF8.GetString(fileData);
            var prompt = $"Analiza el siguiente archivo en busca de indicadores de compromiso y proporciona un resumen y una descripción detallada:\n\n{fileContent}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var jsonContent = JsonSerializer.Serialize(new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = 500
            });

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(OpenAIUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            // Divide el resultado en resumen y descripción. (Aquí simularemos una división)
            var resumen = result.Substring(0, Math.Min(100, result.Length)); // Por ejemplo, los primeros 100 caracteres
            var descripcion = result; // Todo el contenido del análisis
            return (resumen, descripcion);
        }
    }
}