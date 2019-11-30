using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Users
{
    public class ProfileUpdate
    {
        public class Command : IRequest<ProfileDetailsDto>
        {
            public Command(
                string firstName,
                string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).NotEmpty().NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, ProfileDetailsDto>
        {
            private readonly ExpensesContext _context;
            private readonly ICurrentUser _currentUser;

            public Handler(
                ExpensesContext db,
                ICurrentUser currentUser)
            {
                _context = db;
                _currentUser = currentUser;
            }

            public async Task<ProfileDetailsDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(
                    x => x.Email == _currentUser.EmailId,
                    cancellationToken);

                user.UpdateName(
                    request.FirstName,
                    request.LastName);

                await _context.SaveChangesAsync(cancellationToken);

                return new ProfileDetailsDto(
                    user.FirstName,
                    user.LastName,
                    user.FullName);
            }
        }
    }
}