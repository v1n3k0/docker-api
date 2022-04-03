using Cadastro.Repository;
using Cadastro.Service;
using Cadastro.UseCase;
using StackExchange.Redis;
using System.Data;

public static class DependecyInjection
{
    public static IServiceCollection ProgramConfiguration(this IServiceCollection services)
    {
        //UseCase
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        //Service
        services.AddSingleton<ITokenService, TokenService>();
        
        //Conection
        services.AddTransient<IDbConnection>(x => new System.Data.SqlClient.SqlConnection("Server=localhost;Database=Cadastro;User Id=sa;Password=Password123"));
        services.AddTransient<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect("localhost"));

        //Repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        return services;
    }
}