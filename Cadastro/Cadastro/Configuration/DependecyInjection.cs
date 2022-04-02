using Cadastro.Repository;
using Cadastro.UseCase;
using System.Data;

public static class DependecyInjection
{
    public static IServiceCollection ProgramConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddTransient<IDbConnection>(x => new System.Data.SqlClient.SqlConnection("Server=localhost;Database=Cadastro;User Id=sa;Password=Password123"));
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IRefreshTokenUseCase, RefreshTokenUseCase>();

        return services;
    }
}