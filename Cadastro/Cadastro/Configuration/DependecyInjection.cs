using Cadastro;
using Cadastro.Repository;
using Cadastro.Service;
using Cadastro.UseCase;
using StackExchange.Redis;
using System.Data;

public static class DependecyInjection
{
    public static IServiceCollection ProgramConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = new ConnectionString();
        configuration.GetSection("ConnectionString").Bind(connectionString);

        var user = Environment.GetEnvironmentVariable("DbUser");
        var password = Environment.GetEnvironmentVariable("DbPassword");

        connectionString.DbCadastro = connectionString.DbCadastro
            .Replace("UserDB", user)
            .Replace("PasswordDB", password);

        //UseCase
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        //Service
        services.AddSingleton<ITokenService, TokenService>();
        
        //Conection
        services.AddTransient<IDbConnection>(x => new System.Data.SqlClient.SqlConnection(connectionString.DbCadastro));
        services.AddTransient<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(connectionString.DbRedis));

        //Repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}