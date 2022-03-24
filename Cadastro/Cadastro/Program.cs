using Cadastro;
using Cadastro.Models;
using Cadastro.Repository;
using Cadastro.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(Settings.Secret);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("manager"));
    options.AddPolicy("Employee", policy => policy.RequireRole("employee"));
});

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


app.MapPost("/login", (User model) =>
 {
     var user = UserRepository.Get(model.Username, model.Password);

     if(user is null)
         return Results.NotFound(new {message = "Inválido username or password"});

     var token = TokenService.GenerateToken(user);

     user.Password = string.Empty;

     return Results.Ok(new
     {
         user = user,
         token = token
     });
 });

app.MapGet("/anonymous", () => Results.Ok("Anonymous")).AllowAnonymous();

app.MapGet("/autenticated", (ClaimsPrincipal user) =>
 {
     return Results.Ok($"Authenticated as {user.Identity.Name}");
 }).RequireAuthorization();

app.MapGet("/manager", (ClaimsPrincipal user) =>
{
    return Results.Ok($"Authenticated as {user.Identity.Name}");
}).RequireAuthorization("Admin");

app.MapGet("/employee", (ClaimsPrincipal user) =>
{
    return Results.Ok($"Authenticated as {user.Identity.Name}");
}).RequireAuthorization("Employee");

app.Run();