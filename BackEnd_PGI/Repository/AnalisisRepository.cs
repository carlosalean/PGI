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
    public class AnalisisRepository : IAnalisisRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalisisRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Analisis>> GetAllAsync()
        {
            return await _context.Analisis.ToListAsync();
        }

        public async Task<Analisis> GetByIdAsync(int id)
        {
            return await _context.Analisis.FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<Analisis> CreateAsync(Analisis analisis)
        {
            _context.Analisis.Add(analisis);
            await _context.SaveChangesAsync();
            return analisis;
        }

        public async Task UpdateAsync(Analisis analisis)
        {
            _context.Entry(analisis).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var analisisToDelete = await _context.Analisis.FindAsync(id);
            if (analisisToDelete != null)
            {
                _context.Analisis.Remove(analisisToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
