using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace vue_expenses_api.Infrastructure.Security
{
    public interface IJwtSigningCredentials
    {
        SigningCredentials SigningCredentials { get; }
        string Issuer { get; }
        string Audience { get; }
    }


    public class JwtSigningCredentials : IJwtSigningCredentials
    {
        private readonly IConfiguration _configuration;

        public JwtSigningCredentials(
            IConfiguration configuration)
        {
            _configuration = configuration;

            var signingKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecurityKey"]));

            SigningCredentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256Signature);
            Issuer = _configuration["JwtSettings:Issuer"];
            Audience = _configuration["JwtSettings:Audience"];
        }

        public SigningCredentials SigningCredentials { get; }
        public string Issuer { get; }
        public string Audience { get; }
    }
}