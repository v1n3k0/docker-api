
using Cadastro.UseCase;

public static class DependecyInjection
{
    public static IServiceCollection ProgramConfiguration(this IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();


        return services;
    }
}