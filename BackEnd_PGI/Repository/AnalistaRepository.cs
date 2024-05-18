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
    public class AnalistaRepository : IAnalistaRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Analista>> GetAllAsync()
        {
            return await _context.Analistas.ToListAsync();
        }

        public async Task<Analista> GetByIdAsync(int id)
        {
            return await _context.Analistas.FindAsync(id);
        }

        public async Task<Analista> CreateAsync(Analista analista)
        {
            _context.Analistas.Add(analista);
            await _context.SaveChangesAsync();
            return analista;
        }

        public async Task UpdateAsync(Analista analista)
        {
            _context.Entry(analista).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var analistaToDelete = await _context.Analistas.FindAsync(id);
            if (analistaToDelete != null)
            {
                _context.Analistas.Remove(analistaToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
