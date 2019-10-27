using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expense_api.Dtos;

namespace vue_expense_api.Features.Expenses
{
    public class ExpenseList
    {
        public class Query : IRequest<List<ExpenseDto>>
        {
        }

        public class QueryHandler : IRequestHandler<Query, List<ExpenseDto>>
        {
            private readonly IDbConnection _dbConnection;

            public QueryHandler(
                IDbConnection connection)
            {
                _dbConnection = connection;
            }

            //Implement Paging
            public async Task<List<ExpenseDto>> Handle(
                Query message,
                CancellationToken cancellationToken)
            {
                var sql = @"SELECT 
                                Id,
                                ec.Name AS Category,
                                ec.Id AS CategoryId,
                                et.Name AS Type,
                                et.Id AS TypeId,
                                Value,
                                Comments
                            FROM 
                                Expense e
                            INNER JOIN
                                ExpenseCategory ec ON ec.Id = e.CategoryID
                            INNER JOIN
                                ExpenseType et ON et.Id = e.TypeID";
                var expenses = await _dbConnection.QueryAsync<ExpenseDto>(
                    sql);
                return expenses.ToList();
            }
        }
    }
}