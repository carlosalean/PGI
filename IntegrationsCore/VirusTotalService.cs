using Microsoft.Extensions.Options;
using System.Text.Json;

namespace IntegrationsCore
{
    public class VirusTotalOptions
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class VirusTotalService : IIOCService
    {
        private readonly string _apiKey;
        private const string VirusTotalUploadUrl = "https://www.virustotal.com/api/v3/files";
        private const string VirusTotalReportUrl = "https://www.virustotal.com/api/v3/analyses/";

        public VirusTotalService(IOptions<OpenAIOptions> options)
        {
            _apiKey = options.Value.ApiKey;
        }

        public async Task<(string resumen, string descripcion)> AnalyzeFileAsync(byte[] fileData, string fileName)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", _apiKey);

            // Paso 1: Enviar archivo para análisis
            var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(fileData), "file", fileName }
        };

            var uploadResponse = await client.PostAsync(VirusTotalUploadUrl, content);
            uploadResponse.EnsureSuccessStatusCode();

            // Obtener el ID del análisis
            var uploadResult = await uploadResponse.Content.ReadAsStringAsync();
            var analysisId = JsonSerializer.Deserialize<VirusTotalUploadResponse>(uploadResult)?.Data?.Id;

            if (string.IsNullOrEmpty(analysisId))
                throw new Exception("Error al obtener el ID de análisis de VirusTotal.");

            // Paso 2: Consultar el resultado del análisis usando el ID
            var reportResponse = await client.GetAsync($"{VirusTotalReportUrl}{analysisId}");
            reportResponse.EnsureSuccessStatusCode();

            var reportResult = await reportResponse.Content.ReadAsStringAsync();
            var reportData = JsonSerializer.Deserialize<VirusTotalReportResponse>(reportResult);

            // Procesar el resumen y descripción a partir de los datos recibidos
            var resumen = $"Archivo '{fileName}' contiene posibles amenazas.";
            var descripcion = JsonSerializer.Serialize(reportData); // Serializar el resultado completo como descripción detallada
            return (resumen, descripcion);
        }

        public class VirusTotalUploadResponse
        {
            public UploadData? Data { get; set; }

            public class UploadData
            {
                public string? Id { get; set; }
            }
        }
    }
    public class VirusTotalReportResponse
    {
        public ReportData? Data { get; set; }

        public class ReportData
        {
            public Attributes? Attributes { get; set; }
        }

        public class Attributes
        {
            public Stats? Stats { get; set; }
            public string? Status { get; set; }
        }

        public class Stats
        {
            public int Harmless { get; set; }
            public int Malicious { get; set; }
            public int Suspicious { get; set; }
            public int Undetected { get; set; }
        }
    }

}
