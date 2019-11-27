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
using vue_expenses_api.Infrastructure.Exceptions;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api
{
    public static class StartupExtensions
    {
        public static void AddJwt(
            this IServiceCollection services)
        {
            services.AddOptions();

            var signingCredentials = services.BuildServiceProvider().GetService<IJwtSigningCredentials>();

            services.Configure<JwtIssuerOptions>(
                options =>
                {
                    options.Issuer = signingCredentials.Issuer;
                    options.Audience = signingCredentials.Audience;
                    options.SigningCredentials = signingCredentials.SigningCredentials;
                });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingCredentials.SigningCredentials.Key,
                ValidateIssuer = true,
                ValidIssuer = signingCredentials.Issuer,
                ValidateAudience = true,
                ValidAudience = signingCredentials.Audience,
                ValidateLifetime = true
            };

            services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
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
                        },
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add(
                                    "Token-Expired",
                                    "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }

        public static void AddSerilogLogging(
            this ILoggerFactory loggerFactory)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}",
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