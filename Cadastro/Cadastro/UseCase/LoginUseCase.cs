using Cadastro.Models;
using Cadastro.Repository;
using Cadastro.Service;

namespace Cadastro.UseCase
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository userRepository;

        public LoginUseCase(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<UserModel> ExecuteAsync(UserModel request)
        {
            var user = await userRepository.GetAsync(request.Username, request.Password);

            if(user == null)
                return null;

            var token = TokenService.GenerateToken(user);
            var refreshToken = TokenService.GenerateRefreshToken();
            TokenService.SaveRefreshToken(user.Username, refreshToken);

            user.Token = token;
            user.Password = string.Empty;
            user.RefreshToken = refreshToken;

            return user;
        }
    }
}
