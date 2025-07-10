namespace IntegrationsCore
{
    public interface IIOCService
    {
        Task<(string resumen, string descripcion)> AnalyzeFileAsync(byte[] fileData, string fileName);
    }
}
