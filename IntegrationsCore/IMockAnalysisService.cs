using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationsCore
{
    public interface IMockAnalysisService
    {
        Task<(string resumen, string descripcion)> AnalyzeAssetAsync(Asset asset);
        Task<byte[]> MockDownloadFileFromFtpAsync(string fileName);
    }
}
