using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.ExpenseCategories;

public class ExpenseCategoryList
{
    public class Query : IRequest<List<ExpenseCategoryDto>>
    {
    }

    public class QueryHandler : IRequestHandler<Query, List<ExpenseCategoryDto>>
    {
        private readonly IDbConnection _dbConnection;
        private readonly ICurrentUser _currentUser;

        public QueryHandler(
            IDbConnection connection,
            ICurrentUser currentUser)
        {
            _dbConnection = connection;
            _currentUser = currentUser;
        }

        //Implement Paging
        public async Task<List<ExpenseCategoryDto>> Handle(
            Query message,
            CancellationToken cancellationToken)
        {
            var sql = @"SELECT 
                                ec.*
                            FROM 
                                ExpenseCategories ec
                            INNER JOIN
                                Users u ON u.Id = ec.UserId
                            WHERE 
                                u.Email=@userEmailId
                                AND ec.Archived = 0";

            var expenses = await _dbConnection.QueryAsync<ExpenseCategoryDto>(
                sql,
                new
                {
                    userEmailId = _currentUser.EmailId
                });

            return expenses.ToList();
        }
    }
}