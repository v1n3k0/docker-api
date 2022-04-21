using Cadastro.Entity;
using System.Security.Claims;

namespace Cadastro.Service
{
    public interface ITokenService
    {
        string GenerateToken(UserEntity user);
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
