using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface IAnalisisRepository
    {
        Task<IEnumerable<Analisis>> GetAllAsync();
        Task<Analisis> GetByIdAsync(int id);
        Task<Analisis> CreateAsync(Analisis analisis);
        Task UpdateAsync(Analisis analisis);
        Task DeleteAsync(int id);
    }
}
