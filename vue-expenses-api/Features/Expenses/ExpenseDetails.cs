using System.Data;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Exceptions;

namespace vue_expenses_api.Features.Expenses
{
    public class ExpenseDetails
    {
        public class Query : IRequest<ExpenseDto>
        {
            public Query(
                int id)
            {
                Id = id;
            }

            public int Id { get; }
        }

        public class QueryHandler : IRequestHandler<Query, ExpenseDto>
        {
            private readonly IDbConnection _dbConnection;

            public QueryHandler(
                IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }

            public async Task<ExpenseDto> Handle(
                Query message,
                CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM Expense WHERE Id=@id";
                var expense = await _dbConnection.QuerySingleOrDefaultAsync<ExpenseDto>(
                    sql,
                    new
                    {
                        id = message.Id
                    });

                if (expense == null)
                {
                    throw new HttpException(
                        HttpStatusCode.NotFound,
                        new
                        {
                            Error = "Couldn't find the expense record."
                        });
                }

                return expense;
            }
        }
    }
}