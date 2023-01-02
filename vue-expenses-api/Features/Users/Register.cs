using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expenses_api.Domain;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Exceptions;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Users;

public class Register
{
    public class Command : IRequest
    {
        public Command(
            string email,
            string firstName,
            string lastName,
            string password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command>
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

        public async Task<Unit> Handle(
            Command request,
            CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(
                x => x.Email == request.Email,
                cancellationToken);

            if (user != null)
            {
                throw new HttpException(
                    HttpStatusCode.BadRequest,
                    new
                    {
                        Error = $"There is already a user with email {request.Email}."
                    });
            }


            var salt = Guid.NewGuid().ToByteArray();
            var newUser = new User(
                request.FirstName,
                request.LastName,
                request.Email,
                _passwordHasher.Hash(
                    request.Password,
                    salt),
                salt);

            await _context.Users.AddAsync(
                newUser,
                cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}