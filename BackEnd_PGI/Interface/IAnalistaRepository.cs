using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface IAnalistaRepository
    {
        Task<IEnumerable<Analista>> GetAllAsync();
        Task<Analista> GetByIdAsync(int id);
        Task<Analista> CreateAsync(Analista analista);
        Task UpdateAsync(Analista analista);
        Task DeleteAsync(int id);
    }
}
