using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expenses_api.Domain;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure;

namespace vue_expenses_api.Features.Expenses
{
    public class ExpenseCreate
    {
        public class Command : IRequest<ExpenseDto>
        {
            public Command(
                DateTime date,
                int categoryId,
                int typeId,
                decimal value,
                string comments)
            {
                Date = date;
                CategoryId = categoryId;
                TypeId = typeId;
                Value = value;
                Comments = comments;
            }

            public DateTime Date { get; }
            public int CategoryId { get; }
            public int TypeId { get; }
            public decimal Value { get; }
            public string Comments { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Date).NotNull().NotEmpty();
                RuleFor(x => x.CategoryId).NotNull().NotEmpty();
                RuleFor(x => x.TypeId).NotNull().NotEmpty();
                RuleFor(x => x.Value).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, ExpenseDto>
        {
            private readonly ExpensesContext _context;

            public Handler(
                ExpensesContext db)
            {
                _context = db;
            }

            public async Task<ExpenseDto> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var expense = new Expense(
                    request.Date,
                    _context.ExpenseCateogries.SingleOrDefaultAsync(
                        x => x.Id == request.CategoryId,
                        cancellationToken).Result,
                    _context.ExpenseTypes.SingleOrDefaultAsync(
                        x => x.Id == request.TypeId,
                        cancellationToken).Result,
                    request.Value,
                    request.Comments);

                await _context.Expenses.AddAsync(
                    expense,
                    cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return new ExpenseDto(
                    expense.Id,
                    expense.Category.Id,
                    expense.Type.Id,
                    expense.Value,
                    expense.Comments);
            }
        }
    }
}