using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface ITareaRepository
    {
        Task<IEnumerable<Tarea>> GetAllAsync();
        Task<IEnumerable<Tarea>> GetByIdCasoAsync(int idCaso);
        Task<Tarea> GetByIdAsync(int id);
        Task<Tarea> CreateAsync(Tarea tarea);
        Task UpdateAsync(Tarea tarea);
        Task<bool> DeleteAsync(int id);

    }
}
