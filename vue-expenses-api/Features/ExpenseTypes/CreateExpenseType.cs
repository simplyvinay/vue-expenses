using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expenses_api.Domain;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Exceptions;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.ExpenseTypes
{
    public class CreateExpenseType
    {
        public class Command : IRequest<ExpenseTypeDto>
        {
            public Command(
                string name,
                string description)
            {
                Name = name;
                Description = description;
            }

            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(50);
                RuleFor(x => x.Description).MaximumLength(100);
            }
        }

        public class Handler : IRequestHandler<Command, ExpenseTypeDto>
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

            public async Task<ExpenseTypeDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(x => x.Email == _currentUser.EmailId,
                    cancellationToken);
                
                if (await _context.ExpenseTypes.AnyAsync(
                    x => x.Name == request.Name && x.User == user,
                    cancellationToken))
                {
                    throw new HttpException(
                        HttpStatusCode.BadRequest,
                        new
                        {
                            Error = $"There is already a type with name {request.Name}."
                        });
                }

                var expenseType = new ExpenseType(
                    request.Name,
                    request.Description,
                    user);

                await _context.ExpenseTypes.AddAsync(
                    expenseType,
                    cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new ExpenseTypeDto(
                    expenseType.Id,
                    expenseType.Name,
                    expenseType.Description);
            }
        }
    }
}