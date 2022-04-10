namespace Cadastro
{
    public class ConnectionString
    {
        public ConnectionString()
        {

        }

        public const string NAME  = "ConnectionString";
        public string DbCadastro { get; set; } = string.Empty;
        public string DbRedis { get; set; } = string.Empty;
    }
}
