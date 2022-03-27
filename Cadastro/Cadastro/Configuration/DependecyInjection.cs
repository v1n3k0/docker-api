using Cadastro.UseCase;
using System.Data;

public static class DependecyInjection
{
    public static IServiceCollection ProgramConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddTransient<IDbConnection>(x => new System.Data.SqlClient.SqlConnection("Server=localhost\\SQLEXPRESS;Database=passaro;Trusted_Connection=True;"));

        return services;
    }
}