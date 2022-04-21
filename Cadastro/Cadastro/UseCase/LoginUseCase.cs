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
            var model = new UserModel();

            if(!IsValid(model))
                throw new ArgumentException("Username or Password invalid",nameof(model));

            var user = await _userRepository.GetAsync(request.Username, request.Password);

            if(user == null)
                return model;

            var token = _tokenService.GenerateToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();
            await _refreshTokenRepository.SetAsync(user.Name, refreshToken);

            model = user;
            model.Token = token;
            model.RefreshToken = refreshToken;

            return model;
        }

        private bool IsValid(UserModel model)
        {
            return model != null
                && !string.IsNullOrWhiteSpace(model.Username)
                && !string.IsNullOrWhiteSpace(model.Password);
        }
    }
}
