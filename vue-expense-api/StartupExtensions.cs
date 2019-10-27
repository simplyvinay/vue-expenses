using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using vue_expense_api.Infrastructure.Exceptions;
using vue_expense_api.Infrastructure.Security;

namespace vue_expense_api
{
    public static class StartupExtensions
    {
        public static void AddJwt(
            this IServiceCollection services)
        {
            services.AddOptions();

            //security key
            var securityKey =
                "Nam_interdum_tortor_vel_tempus_malesuada._Donec_efficitur,_nibh_suscipit_fringilla_dapibus,_erat_massa_bibendum_risus,_sed_semper_nulla_leo_et_orci.";

            //symmetric security key
            var signingKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha256Signature);
            var issuer = "issuer";
            var audience = "audience";

            services.Configure<JwtIssuerOptions>(
                options =>
                {
                    options.Issuer = issuer;
                    options.Audience = audience;
                    options.SigningCredentials = signingCredentials;
                });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingCredentials.Key,
                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = issuer,
                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,
                // Validate the token expiry
                ValidateLifetime = true
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options =>
                    {
                        options.TokenValidationParameters = tokenValidationParameters;
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = (
                                context) =>
                            {
                                var token = context.HttpContext.Request.Headers["Authorization"];
                                if (token.Count > 0 && token[0].StartsWith(
                                        "Token ",
                                        StringComparison.OrdinalIgnoreCase))
                                {
                                    context.Token = token[0].Substring("Token ".Length).Trim();
                                }

                                return Task.CompletedTask;
                            }
                        };

                    });
        }

        public static void AddSerilogLogging(
            this ILoggerFactory loggerFactory)
        {
            // Attach the sink to the logger configuration
            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                //just for local debug
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            loggerFactory.AddSerilog(log);
            Log.Logger = log;
        }

        public static IApplicationBuilder UseMiddlewares(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}