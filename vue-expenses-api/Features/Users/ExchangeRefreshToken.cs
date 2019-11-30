using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Exceptions;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Users
{
    public class ExchangeRefreshToken
    {
        public class Command : IRequest<UserDto>
        {
            public Command(
                string token,
                string refreshToken)
            {
                Token = token;
                RefreshToken = refreshToken;
            }

            public string Token { get; set; }
            public string RefreshToken { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.RefreshToken).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly ExpensesContext _context;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private IJwtSigningCredentials _signingCredentials;

            public Handler(
                ExpensesContext context,
                IJwtTokenGenerator jwtTokenGenerator,
                IJwtSigningCredentials signingCredentials)
            {
                _context = context;
                _jwtTokenGenerator = jwtTokenGenerator;
                _signingCredentials = signingCredentials;
            }

            public async Task<UserDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var email = GetIdentifierFromExpiredToken(request.Token).Value;
                
                var user = await _context.Users.Include(u => u.RefreshTokens)
                    .SingleAsync(
                        x => x.Email == email && !x.Archived,
                        cancellationToken);

                if (user == null)
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new {Error = "Invalid credentials."});
                }

                if (!user.IsValidRefreshToken(request.RefreshToken))
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new {Error = "Invalid credentials."});
                }

                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
                user.RemoveRefreshToken(request.RefreshToken);
                user.AddRefreshToken(
                    refreshToken,
                    user.Id);
                var token = await _jwtTokenGenerator.CreateToken(user.Email);
                await _context.SaveChangesAsync(cancellationToken);

                return new UserDto(
                    user.FirstName,
                    user.LastName,
                    user.FullName,
                    user.SystemName,
                    user.Email,
                    token,
                    refreshToken,
                    user.CurrencyRegionName,
                    user.UseDarkMode);
            }


            private Claim GetIdentifierFromExpiredToken(
                string token)
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _signingCredentials.SigningCredentials.Key,
                    ValidateIssuer = true,
                    ValidIssuer = _signingCredentials.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _signingCredentials.Audience,
                    ValidateLifetime = false, // do not check for expiry date time
                    ClockSkew = new TimeSpan(0, 0, 0, 0)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(
                    token,
                    tokenValidationParameters,
                    out var securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Contains(
                        SecurityAlgorithms.HmacSha256Signature,
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }

                return principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            }
        }
    }
}