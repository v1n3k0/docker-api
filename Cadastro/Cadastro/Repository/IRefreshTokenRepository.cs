namespace Cadastro.Repository
{
    public interface IRefreshTokenRepository
    {
        Task DeleteAsync(string username);

        Task<string> GetAsync(string username);

        Task SetAsync(string username, string refreshToken);
    }
}
