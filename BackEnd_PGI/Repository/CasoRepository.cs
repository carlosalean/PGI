using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_PGI.Repository
{
    public class CasoRepository : ICasoRepository
    {
        private readonly ApplicationDbContext _context;

        public CasoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Caso>> GetAllAsync()
        {
            return await _context.Casos.ToListAsync();
        }

        public async Task<Caso> GetByIdAsync(int id)
        {
            return await _context.Casos.FindAsync(id);
        }

        public async Task<Caso> CreateAsync(Caso caso)
        {
            _context.Casos.Add(caso);
            await _context.SaveChangesAsync();
            return caso;
        }

        public async Task UpdateAsync(Caso caso)
        {
            _context.Entry(caso).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var casoToDelete = await _context.Casos.FindAsync(id);
            if (casoToDelete != null)
            {
                _context.Casos.Remove(casoToDelete);
                await _context.SaveChangesAsync();
            }
        }

    }
}
