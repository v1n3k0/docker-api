using Cadastro.Models;
using Dapper;
using System.Data;

namespace Cadastro.Repository
{
    public class UserRepository
    {
        private IDbConnection connection;
        public UserRepository(IDbConnection db)
        {
            connection = db;
        }

        public static UserModel Get(string username, string password)
        {
            var users = new List<UserModel>
            {
                new UserModel { Id = 1, Username = "batman", Password = "123456", Role = "manager" },
                new UserModel { Id = 1, Username = "robin", Password = "123456", Role = "employee" },
            };

            return users.FirstOrDefault(x => x.Username.Equals(username.ToLower()) && x.Password.Equals(password.ToLower()));
        }

        public async Task<UserModel> GetAsync(string username, string password)
        {
            var query = "";


            var result = await connection.QueryFirstOrDefaultAsync<UserModel>(query);

            return result;
        }
    }
}
