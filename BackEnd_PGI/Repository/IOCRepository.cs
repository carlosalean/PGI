using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_PGI.Repository
{
    public class IOCRepository : IIOCRepository
    {
        private readonly ApplicationDbContext _context;

        public IOCRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IOC>> GetAllAsync()
        {
            return await _context.IOCs.ToListAsync();
        }

        public async Task<IOC> GetByIdAsync(int id)
        {
            return await _context.IOCs.FindAsync(id);
        }

        public async Task<IOC> CreateAsync(IOC ioc)
        {
            _context.IOCs.Add(ioc);
            await _context.SaveChangesAsync();
            return ioc;
        }

        public async Task UpdateAsync(IOC ioc)
        {
            _context.Entry(ioc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var iocToDelete = await _context.IOCs.FindAsync(id);
            if (iocToDelete != null)
            {
                _context.IOCs.Remove(iocToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
