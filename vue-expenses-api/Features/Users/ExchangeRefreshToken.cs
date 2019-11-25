using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                string refreshToken)
            {
                RefreshToken = refreshToken;
            }

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
            private readonly ICurrentUser _currentUser;
            private readonly ExpensesContext _context;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;

            public Handler(
                ICurrentUser currentUser,
                ExpensesContext context,
                IJwtTokenGenerator jwtTokenGenerator)
            {
                _currentUser = currentUser;
                _context = context;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

            public async Task<UserDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(u => u.RefreshTokens)
                    .SingleAsync(x => x.Email == _currentUser.EmailId && !x.Archived,
                    cancellationToken);

                if (user == null)
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new { Error = "Invalid credentials." });
                }

                if (!user.IsValidRefreshToken(request.RefreshToken))
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new { Error = "Invalid credentials." });
                }

                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
                user.RemoveRefreshToken(request.RefreshToken);
                user.AddRefreshToken(
                    refreshToken,
                    user.Id);
                var token = await _jwtTokenGenerator.CreateToken(user.Email);
                await _context.SaveChangesAsync(cancellationToken);

                return new UserDto(
                    user.Email,
                    token,
                    refreshToken,
                    false);
            }
        }
    }
}