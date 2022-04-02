using Cadastro.Models;

public interface IUserRepository
{
    Task<UserModel> GetAsync(string username, string password);
}

