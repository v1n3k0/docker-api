using Cadastro.Models;
using Cadastro.UseCase;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AuthenticationConfiguration();
builder.Services.ProgramConfiguration();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/login", (User model, ILoginUseCase usecase) =>
 {
     var user = usecase.ExecuteAsync(model);

     if(user is null)
         return Results.NotFound(new {message = "Inválido username or password"});


     return Results.Ok(new
     {
         user
     });
 });

app.MapGet("/anonymous", () => Results.Ok("Anonymous")).AllowAnonymous();

app.MapGet("/autenticated", (ClaimsPrincipal user) =>
 {
     return Results.Ok($"Authenticated as {user.Identity?.Name}");
 }).RequireAuthorization();

app.MapGet("/manager", (ClaimsPrincipal user) =>
{
    return Results.Ok($"Authenticated as {user.Identity?.Name}");
}).RequireAuthorization("Admin");

app.MapGet("/employee", (ClaimsPrincipal user) =>
{
    return Results.Ok($"Authenticated as {user.Identity?.Name}");
}).RequireAuthorization("Employee");

app.Run();