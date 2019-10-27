using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expense_api.Infrastructure;

namespace vue_expense_api.Features.Expenses
{
    public class ExpenseDelete
    {
        public class Command : IRequest
        {
            public Command(
                int? id)
            {
                Id = id;
            }

            public int? Id { get; }
        }

        public class CommandValidator : AbstractValidator<ExpenseDelete.Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ExpensesContext _context; 

            public Handler(
                ExpensesContext db)
            {
                _context = db;
            }

            public async Task<Unit> Handle(
                Command request,
                CancellationToken cancellationToken)
            {
                var expense = await _context.Expenses.FirstOrDefaultAsync(
                    x => x.Id == request.Id.Value,
                    cancellationToken);

                if (expense == null)
                {
                    throw new Exception("Not Found");
                }

                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}