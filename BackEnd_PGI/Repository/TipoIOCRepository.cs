using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Repository
{
    public class TipoIOCRepository : ITipoIOCRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoIOCRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoIOC>> GetAllAsync()
        {
            return await _context.TipoIOCs.ToListAsync();
        }

        public async Task<TipoIOC> GetByIdAsync(int id)
        {
            return await _context.TipoIOCs.FindAsync(id);
        }

        public async Task<TipoIOC> CreateAsync(TipoIOC tipoIoc)
        {
            _context.TipoIOCs.Add(tipoIoc);
            await _context.SaveChangesAsync();
            return tipoIoc;
        }

        public async Task UpdateAsync(TipoIOC tipoIoc)
        {
            _context.Entry(tipoIoc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tipoIocToDelete = await _context.TipoIOCs.FindAsync(id);
            if (tipoIocToDelete != null)
            {
                _context.TipoIOCs.Remove(tipoIocToDelete);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<TipoIOC?> GetByKeywordAsync(string keyword)
        {
            return await _context.TipoIOCs
                .FirstOrDefaultAsync(t => t.Nombre.Contains(keyword) || t.Descripcion.Contains(keyword));
        }
    }

}
