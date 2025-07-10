using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface IThreatDetectionService
    {
        Task<TipoIOC?> DetectThreatAndGetTipoIOCAsync(int tipoAssetId, string fileContent);
    }
}
