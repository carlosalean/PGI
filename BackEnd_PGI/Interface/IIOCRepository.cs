using BackEnd_PGI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd_PGI.Interface
{
    public interface IIOCRepository
    {
        Task<IEnumerable<IOC>> GetAllAsync();
        Task<IOC> GetByIdAsync(int id);
        Task<IOC> CreateAsync(IOC ioc);
        Task UpdateAsync(IOC ioc);
        Task DeleteAsync(int id);
    }
}
