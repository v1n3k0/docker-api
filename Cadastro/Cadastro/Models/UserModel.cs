using Cadastro.Entity;

namespace Cadastro.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

        public static implicit operator UserModel(UserEntity entity)
        {
            return new UserModel()
            {
                Id = entity.Id,
                Role = entity.Role,
                Username = entity.Name
            };
        }
    }
}
