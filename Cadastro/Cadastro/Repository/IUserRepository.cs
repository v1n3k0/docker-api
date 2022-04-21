using Cadastro.Entity;

public interface IUserRepository
{
    Task<UserEntity> GetAsync(string username, string password);
}

