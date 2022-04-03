using Cadastro.Models;
using Cadastro.Repository;
using Cadastro.Service;

namespace Cadastro.UseCase
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginUseCase(
            IUserRepository userRepository,
            ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<UserModel> ExecuteAsync(UserModel request)
        {
            var user = await _userRepository.GetAsync(request.Username, request.Password);

            if(user == null)
                return null;

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _refreshTokenRepository.SetAsync(user.Username, refreshToken);

            user.Token = token;
            user.Password = string.Empty;
            user.RefreshToken = refreshToken;

            return user;
        }
    }
}
