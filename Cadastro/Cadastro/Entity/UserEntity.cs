namespace Cadastro.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid Code { get; set; }

        public UserEntity(int id, string name, string password, string role, Guid code)
        {
            Id = id;
            Name = name;
            Password = password;
            Role = role;
            Code = code;
        }
    }
}
