using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace vue_expenses_api.Infrastructure.Security;

public interface IJwtTokenGenerator
{
    Task<string> CreateToken(
        string email);

    string GenerateRefreshToken(
        int size = 32);
}

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtIssuerOptions _jwtOptions;

    public JwtTokenGenerator(
        IOptions<JwtIssuerOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<string> CreateToken(
        string email)
    {
        var claims = new[]
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                email),
            new Claim(
                JwtRegisteredClaimNames.Jti,
                await _jwtOptions.JtiGenerator()),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(_jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };
        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            _jwtOptions.NotBefore,
            _jwtOptions.Expiration,
            _jwtOptions.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public string GenerateRefreshToken(
        int size = 32)
    {
        var randomNumber = new byte[size];
        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}