namespace PGI_AF.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task RemoveTokenAsync();
    }
}
