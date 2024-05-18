using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_PGI.Repository
{
    public class MaquinaRepository : IMaquinaRepository
    {
        private readonly ApplicationDbContext _context;

        public MaquinaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Maquina>> GetAllAsync()
        {
            return await _context.Maquinas.ToListAsync();
        }

        public async Task<Maquina> GetByIdAsync(int id)
        {
            return await _context.Maquinas.FindAsync(id);
        }
        
        public async Task<IEnumerable<Maquina>> GetByIdCasoAsync(int idCaso)
        {
            return await _context.Maquinas.Where(t => t.CasoID == idCaso).ToListAsync();
        }

        public async Task<List<Maquina>> GetMaquinasWithAssetsAsync()
        {
            return await _context.Maquinas
                         .Include(m => m.Assets)
                             .ThenInclude(a => a.TipoAsset)
                         .ToListAsync();
        }

        public async Task<Maquina> CreateAsync(Maquina maquina)
        {
            _context.Maquinas.Add(maquina);
            await _context.SaveChangesAsync();
            return maquina;
        }

        public async Task UpdateAsync(Maquina maquina)
        {
            _context.Entry(maquina).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var maquinaToDelete = await _context.Maquinas.FindAsync(id);
            if (maquinaToDelete != null)
            {
                _context.Maquinas.Remove(maquinaToDelete);
                await _context.SaveChangesAsync();
            }
        }

    }
}
