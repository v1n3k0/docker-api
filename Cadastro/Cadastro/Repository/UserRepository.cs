using Cadastro.Models;

namespace Cadastro.Repository
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "batman", Password = "123456", Role = "manager" },
                new User { Id = 1, Username = "robin", Password = "123456", Role = "employee" },
            };

            return users.FirstOrDefault(x => x.Username.Equals(username.ToLower()) && x.Password.Equals(password.ToLower()));
        }
    }
}
