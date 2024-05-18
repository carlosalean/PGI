using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_PGI.Repository
{

    public class TipoAssetsRepository : ITipoAssetRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoAssetsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoAsset>> GetAllAsync()
        {
            return await _context.TipoAssets.ToListAsync();
        }

        public async Task<TipoAsset> GetByIdAsync(int id)
        {
            return await _context.TipoAssets.FindAsync(id);
        }

        public async Task<TipoAsset> CreateAsync(TipoAsset tipoAsset)
        {
            _context.TipoAssets.Add(tipoAsset);
            await _context.SaveChangesAsync();
            return tipoAsset;
        }

        public async Task UpdateAsync(TipoAsset tipoAsset)
        {
            _context.Entry(tipoAsset).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tipoAssetToDelete = await _context.TipoAssets.FindAsync(id);
            if (tipoAssetToDelete != null)
            {
                _context.TipoAssets.Remove(tipoAssetToDelete);
                await _context.SaveChangesAsync();
            }
        }

    }
}
