using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expenses_api.Domain;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Expenses
{
    public class ExpenseUpdate
    {
        public class Command : IRequest<ExpenseDto>
        {
            public Command(
                int id,
                DateTime date,
                int categoryId,
                int typeId,
                decimal value,
                string comments)
            {
                Id = id;
                Date = date;
                CategoryId = categoryId;
                TypeId = typeId;
                Value = value;
                Comments = comments;
            }

            public int Id { get; set; }
            public DateTime Date { get; set; }
            public int CategoryId { get; set; }
            public int TypeId { get; set; }
            public decimal Value { get; set; }
            public string Comments { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
                RuleFor(x => x.Date).NotNull().NotEmpty();
                RuleFor(x => x.CategoryId).NotNull().NotEmpty();
                RuleFor(x => x.TypeId).NotNull().NotEmpty();
                RuleFor(x => x.Value).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ExpenseDto>
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

            public async Task<ExpenseDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleAsync(x => x.Email == _currentUser.EmailId);
                var expense = await _context.Expenses.FirstAsync(
                    x => x.Id == request.Id,
                    cancellationToken);

                var expenseCategory = await _context.ExpenseCategories.SingleAsync(
                        x => x.Id == request.CategoryId,
                        cancellationToken);
                var expenseType = await _context.ExpenseTypes.SingleAsync(
                        x => x.Id == request.TypeId,
                        cancellationToken);

                expense.Date = request.Date;
                expense.Category = expenseCategory;
                expense.Type = expenseType;
                expense.Value = request.Value;
                expense.Comments = request.Comments;

                await _context.SaveChangesAsync(cancellationToken);

                return new ExpenseDto(
                    expense.Id,
                    expense.Date.ToString(),
                    expenseCategory.Id,
                    expenseCategory.Name,
                    expenseCategory.Budget,
                    expenseCategory.ColourHex,
                    expenseType.Id,
                    expenseType.Name,
                    expense.Value,
                    expense.Comments);
            }
        }
    }
}