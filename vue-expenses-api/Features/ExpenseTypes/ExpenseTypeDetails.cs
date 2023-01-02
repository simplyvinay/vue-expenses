using System.Data;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Exceptions;

namespace vue_expenses_api.Features.ExpenseTypes;

public class ExpenseTypeDetails
{
    public class Query : IRequest<ExpenseTypeDto>
    {
        public Query(
            int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class QueryHandler : IRequestHandler<Query, ExpenseTypeDto>
    {
        private readonly IDbConnection _dbConnection;

        public QueryHandler(
            IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ExpenseTypeDto> Handle(
            Query message,
            CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM ExpenseTypes WHERE Id=@id";
            var expense = await _dbConnection.QuerySingleOrDefaultAsync<ExpenseTypeDto>(
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
                        Error = "Couldn't find the expense type record."
                    });
            }

            return expense;
        }
    }
}