using Cadastro.Models;

namespace Cadastro.UseCase
{
    public interface IRefreshTokenUseCase : IUseCaseAsync<RefreshTokenModel, RefreshTokenModel>
    {
    }
}
