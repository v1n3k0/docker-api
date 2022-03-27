﻿using Cadastro;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class Authentication
{
    public static IServiceCollection AuthenticationConfiguration(this IServiceCollection services)
    {
        var variable = Environment.GetEnvironmentVariable(Settings.Secret) ?? string.Empty;
        var key = Encoding.ASCII.GetBytes(variable);
        services.AddAuthentication(x =>
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

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("manager"));
            options.AddPolicy("Employee", policy => policy.RequireRole("employee"));
        });

        return services;
    }
}
