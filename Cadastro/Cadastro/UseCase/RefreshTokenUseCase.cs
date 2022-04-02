using Cadastro.Models;
using Cadastro.Service;
using Microsoft.IdentityModel.Tokens;

namespace Cadastro.UseCase
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        public RefreshTokenModel Execute(RefreshTokenModel request)
        {
            var principal = TokenService.GetPrincipalFromExpiredToken(request.Token);
            var username = principal.Identity.Name;
            var savedRefreshToken = TokenService.GetRefreshToken(username);
            if (savedRefreshToken != request.RefreshToken)
                throw new SecurityTokenException("Invalid refresh token");
            var newJwtToken = TokenService.GenerateToken(principal.Claims);
            var newRefreshToken = TokenService.GenerateRefreshToken();
            TokenService.DeleteRefreshToken(username, request.RefreshToken);
            TokenService.SaveRefreshToken(username, newRefreshToken);

            return new RefreshTokenModel { 
                Token = newJwtToken, 
                RefreshToken = newRefreshToken 
            };
        }
    }
}
