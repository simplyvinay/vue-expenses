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
                string systemName,
                string currencyRegionName,
                bool useDarkMode)
            {
                SystemName = systemName;
                CurrencyRegionName = currencyRegionName;
                UseDarkMode = useDarkMode;
            }

            public string SystemName { get; set; }
            public string CurrencyRegionName { get; set; }
            public bool UseDarkMode { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.SystemName).NotEmpty().NotNull();
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
                var user = await _context.Users.SingleAsync(
                    x => x.Email == _currentUser.EmailId,
                    cancellationToken);
                user.SystemName = request.SystemName;
                user.CurrencyRegionName = request.CurrencyRegionName;
                user.UseDarkMode = request.UseDarkMode;

                await _context.SaveChangesAsync(cancellationToken);

                return new UserDetailsDto(
                    user.SystemName,
                    user.CurrencyRegionName,
                    user.UseDarkMode);
            }
        }
    }
}