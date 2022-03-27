using Cadastro.Models;
using Cadastro.Repository;
using Cadastro.Service;

namespace Cadastro.UseCase
{
    public class LoginUseCase : ILoginUseCase
    {
        public UserModel? ExecuteAsync(UserModel request)
        {
            var user = UserRepository.Get(request.Username, request.Password);

            if(user == null)
                return null;

            var token = TokenService.GenerateToken(user);

            user.Token = token;
            user.Password = string.Empty;

            return user;
        }
    }
}
