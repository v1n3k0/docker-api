using Cadastro;
using Cadastro.Models;
using Cadastro.Service;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService : ITokenService
{
    public string GenerateToken(UserModel user)
    {
        var variable = Environment.GetEnvironmentVariable(Settings.Secret) ?? string.Empty;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(variable);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var variable = Environment.GetEnvironmentVariable(Settings.Secret) ?? string.Empty;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(variable);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var ramdomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(ramdomNumber);
        return Convert.ToBase64String(ramdomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var variable = Environment.GetEnvironmentVariable(Settings.Secret) ?? string.Empty;
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(variable)),
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    //private List<(string, string)> _refreshTokens = new();

    //public void SaveRefreshToken(string username, string refreshToken)
    //{
    //    _refreshTokens.Add(new(username, refreshToken));
    //}

    //public string GetRefreshToken(string username)
    //{
    //    return _refreshTokens.FirstOrDefault(x => x.Item1 == username).Item2;
    //}

    //public void DeleteRefreshToken(string username, string refreshToken)
    //{
    //    var item = _refreshTokens.FirstOrDefault(x => x.Item1 == username && x.Item2 == refreshToken);
    //    _refreshTokens.Remove(item);
    //}
}
