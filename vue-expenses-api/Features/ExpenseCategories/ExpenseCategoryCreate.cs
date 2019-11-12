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

namespace vue_expenses_api.Features.ExpenseCategories
{
    public class ExpenseCategoryCreate
    {
        public class Command : IRequest<ExpenseCategoryDto>
        {
            public Command(
                string name,
                string description,
                decimal budget,
                string colourHex)
            {
                Name = name;
                Description = description;
                Budget = budget;
                ColourHex = colourHex;
            }

            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Budget { get; set; }
            public string ColourHex { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ExpenseCategoryDto>
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

            public async Task<ExpenseCategoryDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(x => x.Email == _currentUser.EmailId,
                    cancellationToken);

                if (await _context.ExpenseCategories.AnyAsync(
                    x => x.Name == request.Name && x.User == user,
                    cancellationToken))
                {
                    throw new HttpException(
                        HttpStatusCode.BadRequest,
                        new
                        {
                            Error = $"There is already a category with name {request.Name}."
                        });
                }
                
                var expenseCategory = new ExpenseCategory(
                    request.Name,
                    request.Description,
                    request.Budget,
                    request.ColourHex,
                    user);

                await _context.ExpenseCategories.AddAsync(
                    expenseCategory,
                    cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new ExpenseCategoryDto(
                    expenseCategory.Id,
                    expenseCategory.Name,
                    expenseCategory.Description,
                    expenseCategory.Budget,
                    expenseCategory.ColourHex);
            }
        }
    }
}