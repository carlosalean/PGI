using BackEnd_PGI.Model;

namespace BackEnd_PGI.Interface
{
    public interface ICasoRepository
    {
        Task<IEnumerable<Caso>> GetAllAsync();
        Task<Caso> GetByIdAsync(int id);
        Task<Caso> CreateAsync(Caso caso);
        Task UpdateAsync(Caso caso);
        Task DeleteAsync(int id);

    }
}
