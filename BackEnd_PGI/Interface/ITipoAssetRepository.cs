using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface ITipoAssetRepository
    {
        Task<IEnumerable<TipoAsset>> GetAllAsync();
        Task<TipoAsset> GetByIdAsync(int id);
        Task<TipoAsset> CreateAsync(TipoAsset tipoAsset);
        Task UpdateAsync(TipoAsset tipoAsset);
        Task DeleteAsync(int id);

    }

}
