using Cadastro.Models;
using System.Security.Claims;

namespace Cadastro.Service
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
        string GenerateToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
