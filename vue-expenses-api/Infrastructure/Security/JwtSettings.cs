using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace vue_expenses_api.Infrastructure.Security;

public class JwtSettings
{
    public string SecurityKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }

    public SigningCredentials SigningCredentials =>
        new(
            new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(SecurityKey)),
            SecurityAlgorithms.HmacSha256Signature);
}