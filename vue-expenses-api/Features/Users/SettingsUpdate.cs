using System.Globalization;
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
                //demo site hosted on linux container falls over for certain regions, works ok on dev box
                //so check if the selected currency region can be converted to a region
                //if not throw an error. 
                try
                {
                    new RegionInfo(request.CurrencyRegionName);
                }
                catch
                {
                    throw new HttpException(
                        HttpStatusCode.BadRequest,
                        new { Error = "Display currency is not yet supported" });
                }

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