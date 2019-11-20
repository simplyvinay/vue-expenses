using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using vue_expenses_api.Dtos;
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
            private readonly IDbConnection _dbConnection;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;

            public Handler(
                IPasswordHasher passwordHasher,
                IJwtTokenGenerator jwtTokenGenerator,
                IDbConnection dbConnection)
            {
                _passwordHasher = passwordHasher;
                _jwtTokenGenerator = jwtTokenGenerator;
                _dbConnection = dbConnection;
            }

            public async Task<UserDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var sql = @"SELECT 
                                * 
                            FROM 
                                [Users] 
                            WHERE 
                                Email=@email 
                                AND Archived = 0";
                var customerDto = await _dbConnection.QuerySingleOrDefaultAsync<UserDto>(
                    sql,
                    new {email = request.Email});

                if (customerDto == null)
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new {Error = "Invalid credentials."});
                }

                if (!customerDto.Hash.SequenceEqual(
                    _passwordHasher.Hash(
                        request.Password,
                        customerDto.Salt)))
                {
                    throw new HttpException(
                        HttpStatusCode.Unauthorized,
                        new {Error = "Invalid credentials."});
                }

                customerDto.Token = await _jwtTokenGenerator.CreateToken(request.Email);
                return customerDto;
            }
        }
    }
}