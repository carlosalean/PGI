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
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public AssetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            return await _context.Assets.ToListAsync();
        }

        public async Task<Asset> GetAssetWithIOCsAsync(int Id)
        {
            return await _context.Assets
                         .Include(m => m.IOCs)
                             .ThenInclude(a => a.TipoIOC)
                         .FirstOrDefaultAsync(a => a.ID == Id);
        }

        public async Task<IEnumerable<Asset>> GetByIdCasoAsync(int idCaso)
        {
            return await _context.Assets.Where(t => t.CasoID == idCaso).ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetByIdMaquinaAsync(int idMaquina)
        {
            return await _context.Assets.Where(t => t.MaquinaID == idMaquina).ToListAsync();
        }

        public async Task<Asset> GetByIdAsync(int id)
        {
            return await _context.Assets.FirstOrDefaultAsync(a => a.ID == id);
        }

        public async Task<Asset> CreateAsync(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
            return asset;
        }

        public async Task UpdateAsync(Asset asset)
        {
            _context.Entry(asset).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var assetToDelete = await _context.Assets.FindAsync(id);
            if (assetToDelete != null)
            {
                _context.Assets.Remove(assetToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
