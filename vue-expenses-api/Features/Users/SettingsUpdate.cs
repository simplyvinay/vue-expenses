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
    public class SettingsUpdate
    {
        public class Command : IRequest<UserDetailsDto>
        {
            public Command(
                bool useDarkMode)
            {
                UseDarkMode = useDarkMode;
            }

            public bool UseDarkMode { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {

            }
        }

        public class Handler : IRequestHandler<Command, UserDetailsDto>
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

            public async Task<UserDetailsDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(x => x.Email == _currentUser.EmailId);
                user.UseDarkMode = request.UseDarkMode;

                await _context.SaveChangesAsync(cancellationToken);

                return new UserDetailsDto(
                    user.UseDarkMode);
            }
        }
    }
}