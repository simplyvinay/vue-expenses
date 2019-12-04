using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vue_expenses_api.Infrastructure;

namespace vue_expenses_api.Features.Expenses
{
    public class DeleteExpense
    {
        public class Command : IRequest
        {
            public Command(
                int id)
            {
                Id = id;
            }

            public int Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<DeleteExpense.Command>
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
                    x => x.Id == request.Id,
                    cancellationToken);

                if (expense == null)
                {
                    throw new Exception("Not Found");
                }

                expense.Archive();
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}