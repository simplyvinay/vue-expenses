using System.Linq;
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
    public class Login
    {
        public class Command : IRequest<UserDto>
        {
            public Command(
                string email,
                string password)
            {
                Email = email;
                Password = password;
            }

            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Email).NotNull().NotEmpty();
                RuleFor(x => x.Password).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly IPasswordHasher _passwordHasher;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly ExpensesContext _context;

            public Handler(
                IPasswordHasher passwordHasher,
                IJwtTokenGenerator jwtTokenGenerator,
                ExpensesContext context)
            {
                _passwordHasher = passwordHasher;
                _jwtTokenGenerator = jwtTokenGenerator;
                _context = context;
            }

            public async Task<UserDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(
                    x => x.Email == request.Email && !x.Archived,
                    cancellationToken);

                if (user == null)
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new {Error = "Invalid credentials."});
                }

                if (!user.Hash.SequenceEqual(
                    _passwordHasher.Hash(
                        request.Password,
                        user.Salt)))
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new {Error = "Invalid credentials."});
                }
                // generate refresh token
                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
                user.AddRefreshToken(refreshToken, user.Id);
                
                var token = await _jwtTokenGenerator.CreateToken(request.Email);
                await _context.SaveChangesAsync(cancellationToken);
                
                return new UserDto(
                    user.Email,
                    token,
                    refreshToken,
                    user.UseDarkMode);
            }
        }
    }
}