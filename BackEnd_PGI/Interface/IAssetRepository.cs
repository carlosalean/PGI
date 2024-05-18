using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset> GetByIdAsync(int id);
        Task<IEnumerable<Asset>> GetByIdCasoAsync(int idCaso);
        Task<IEnumerable<Asset>> GetByIdMaquinaAsync(int idMaquina);
        Task<Asset> GetAssetWithIOCsAsync(int Id);
        Task<Asset> CreateAsync(Asset asset);
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(int id);
    }
}
