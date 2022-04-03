using StackExchange.Redis;

namespace Cadastro.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RefreshTokenRepository(
            IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task DeleteAsync(string username)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringGetDeleteAsync(username);
        }

        public async Task<string> GetAsync(string username)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return  await db.StringGetAsync(username);
        }

        public async Task SetAsync(string username, string refreshToken)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(username, refreshToken);
        }
    }
}
