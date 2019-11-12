using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.ExpenseTypes
{
    public class ExpenseTypeList
    {
        public class Query : IRequest<List<ExpenseTypeDto>>
        {
        }

        public class QueryHandler : IRequestHandler<Query, List<ExpenseTypeDto>>
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
            public async Task<List<ExpenseTypeDto>> Handle(
                Query message,
                CancellationToken cancellationToken)
            {
                var sql = @"SELECT 
                                *
                            FROM 
                                ExpenseTypes et
                            INNER JOIN
                                Users u ON u.Id = et.UserId
                            WHERE 
                                u.Email=@userEmailId";

                var expenses = await _dbConnection.QueryAsync<ExpenseTypeDto>(
                    sql,
                    new
                    {
                        userEmailId = _currentUser.EmailId
                    });

                return expenses.ToList();
            }
        }
    }
}