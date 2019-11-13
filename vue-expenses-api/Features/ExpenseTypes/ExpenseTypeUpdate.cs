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
    public class ExpenseTypeUpdate
    {
        public class Command : IRequest<ExpenseTypeDto>
        {
            public Command(
                int id,
                string name,
                string description)
            {
                Id = id;
                Name = name;
                Description = description;
            }

            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
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
                var user = await _context.Users.SingleAsync(x => x.Email == _currentUser.EmailId);
                
                if (await _context.ExpenseCategories.AnyAsync(
                    x => x.Name == request.Name && x.User == user && x.Id != request.Id,
                    cancellationToken))
                {
                    throw new HttpException(
                        HttpStatusCode.BadRequest,
                        new
                        {
                            Error = $"There is already a type with name {request.Name}."
                        });
                }

                var expenseType = await _context.ExpenseTypes.FirstAsync(
                    x => x.Id == request.Id,
                    cancellationToken);
                expenseType.Name = request.Name;
                expenseType.Description = request.Description;

                await _context.SaveChangesAsync(cancellationToken);

                return new ExpenseTypeDto(
                    expenseType.Id,
                    expenseType.Name,
                    expenseType.Description);
            }
        }
    }
}