using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_PGI.Repository
{
    public class TareaRepository : ITareaRepository
    {
        private readonly ApplicationDbContext _context;

        public TareaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarea>> GetAllAsync()
        {
            return await _context.Tareas.ToListAsync();
        }

        public async Task<IEnumerable<Tarea>> GetByIdCasoAsync(int idCaso)
        {
            return await _context.Tareas.Where(t => t.CasoID == idCaso).ToListAsync();
        }
        
        public async Task<Tarea> GetByIdAsync(int id)
        {
            return await _context.Tareas.FindAsync(id);
        }

        public async Task<Tarea> CreateAsync(Tarea tarea)
        {
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return tarea;
        }

        public async Task UpdateAsync(Tarea tarea)
        {
            var tareaToUpdate = await _context.Tareas.FirstOrDefaultAsync(t => t.ID == tarea.ID);
            if (tareaToUpdate != null)
            {
                tareaToUpdate.CasoID = tarea.CasoID;
                tareaToUpdate.Descripcion = tarea.Descripcion;
                tareaToUpdate.Estado = tarea.Estado;
                tareaToUpdate.FechaInicio = tarea.FechaInicio;
                tareaToUpdate.FechaFin = tarea.FechaFin;
                tareaToUpdate.DeadLine = tarea.DeadLine;
                tareaToUpdate.AnalistaID = tarea.AnalistaID;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mreturn = false;
            var tareaToDelete = await _context.Tareas.FindAsync(id);
            if (tareaToDelete != null)
            {
                _context.Tareas.Remove(tareaToDelete);
                await _context.SaveChangesAsync();
                mreturn = true;
            }
            return mreturn;
        }


    }
}
