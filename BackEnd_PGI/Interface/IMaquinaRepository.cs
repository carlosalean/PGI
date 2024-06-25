using BackEnd_PGI.Model;

namespace BackEnd_PGI.Interface
{
    public interface IMaquinaRepository
    {
        Task<IEnumerable<Maquina>> GetAllAsync();
        Task<Maquina> GetByIdAsync(int id);
        Task<Maquina> CreateAsync(Maquina maquina);
        Task<List<Maquina>> GetMaquinasWithAssetsAsync(int idCaso);
        Task<IEnumerable<Maquina>> GetByIdCasoAsync(int idCaso);
        Task UpdateAsync(Maquina maquina);
        Task DeleteAsync(int id);

    }
}
