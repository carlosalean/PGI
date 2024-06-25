using BackEnd_PGI.Model;

namespace PGI_AF.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<bool> RegisterAsync(Usuario usuario);
        Task<bool> IsUserAuthenticated();
        Task<string> GetToken();
    }

}
