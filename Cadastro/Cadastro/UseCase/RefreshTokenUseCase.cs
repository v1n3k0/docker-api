using Cadastro.Models;
using Cadastro.Repository;
using Cadastro.Service;
using Microsoft.IdentityModel.Tokens;

namespace Cadastro.UseCase
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenUseCase(
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshTokenModel> ExecuteAsync(RefreshTokenModel request)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var username = principal.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                throw new SecurityTokenException("Invalid token");

            var savedRefreshToken = await _refreshTokenRepository.GetAsync(username);
            if (savedRefreshToken != request.RefreshToken)
                throw new SecurityTokenException("Invalid refresh token");

            var newJwtToken = _tokenService.GenerateToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            await _refreshTokenRepository.DeleteAsync(username);
            await _refreshTokenRepository.SetAsync(username, newRefreshToken);

            return new RefreshTokenModel { 
                Token = newJwtToken, 
                RefreshToken = newRefreshToken 
            };
        }
    }
}
