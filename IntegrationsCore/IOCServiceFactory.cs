using BackEnd_PGI.Model;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationsCore
{
    public class IOCServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public IOCServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IIOCService GetService(BuscarEn buscarEn)
        {
            return buscarEn switch
            {
                BuscarEn.OpenIA => _serviceProvider.GetRequiredService<OpenAIService>(),
                BuscarEn.AlienVault => _serviceProvider.GetRequiredService<AlienVaultService>(),
                BuscarEn.VirusTotal => _serviceProvider.GetRequiredService<VirusTotalService>(),
                _ => throw new ArgumentException("Servicio no encontrado para el tipo de IOC especificado")
            };
        }
    }

}
