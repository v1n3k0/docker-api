using Cadastro.Entity;
using Dapper;
using System.Data;

namespace Cadastro.Repository
{
    public class UserRepository : IUserRepository
    {
        private IDbConnection connection;
        public UserRepository(IDbConnection db)
        {
            connection = db;
        }

        public async Task<UserEntity> GetAsync(string username, string password)
        {
            var query = @"SELECT [IDUSUARIO] as Id
                  ,[NOME] as Name
                  ,[SENHA] as Password
                  ,[REGRA] as Role
                  ,[CODIGO] as Code
              FROM[dbo].[USUARIO]
              where NOME like @username and SENHA like @password";

            var param = new DynamicParameters();
            param.Add("username", username);
            param.Add("password", password);

            var result = await connection.QueryFirstOrDefaultAsync<UserEntity>(query, param);

            return result;
        }
    }
}
