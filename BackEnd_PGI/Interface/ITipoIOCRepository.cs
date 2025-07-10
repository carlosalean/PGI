using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface ITipoIOCRepository
    {
        Task<IEnumerable<TipoIOC>> GetAllAsync();
        Task<TipoIOC> GetByIdAsync(int id);
        Task<TipoIOC> CreateAsync(TipoIOC tipoIoc);
        Task UpdateAsync(TipoIOC tipoIoc);
        Task DeleteAsync(int id);
        Task<TipoIOC?> GetByKeywordAsync(string keyword);
    }
}
