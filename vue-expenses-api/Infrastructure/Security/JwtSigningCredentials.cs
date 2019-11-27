using System.Text;
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
        public JwtSigningCredentials()
        {
            //security key
            var securityKey =
                "Nam_interdum_tortor_vel_tempus_malesuada._Donec_efficitur,_nibh_suscipit_fringilla_dapibus,_erat_massa_bibendum_risus,_sed_semper_nulla_leo_et_orci.";

            //symmetric security key
            var signingKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

            //signing credentials
            SigningCredentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256Signature);
            Issuer = "issuer";
            Audience = "audience";
        }

        public SigningCredentials SigningCredentials { get; }
        public string Issuer { get; }
        public string Audience { get; }
    }
}