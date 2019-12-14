using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Statistics
{
    public class TypesStatisticsList
    {
        public class Query : IRequest<List<TypeStatisticsDto>>
        {
            public int Year { get; }
            public int? Month { get; }

            public Query(
                int year,
                int? month)
            {
                Year = year;
                Month = month;
            }
        }

        public class QueryHandler : IRequestHandler<Query, List<TypeStatisticsDto>>
        {
            private readonly IDbConnection _dbConnection;
            private readonly ICurrentUser _currentUser;

            public QueryHandler(
                IDbConnection dbConnection,
                ICurrentUser currentUser)
            {
                _dbConnection = dbConnection;
                _currentUser = currentUser;
            }

            public async Task<List<TypeStatisticsDto>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var monthConstraint = request.Month.HasValue
                    ? $" AND CAST (STRFTIME('%m', e.Date) AS INT) = {request.Month.Value} "
                    : string.Empty;
                
                var sql = $@"SELECT 
	                            et.Id AS Id,
	                            et.Name AS Name,
	                            CAST(SUM(COALESCE(e.Value, 0)) AS REAL) AS Spent
                            FROM 
	                            ExpenseTypes et
                            INNER JOIN
	                            Users u ON u.Id = et.UserId
                            LEFT JOIN
	                            Expenses e ON e.TypeId = et.Id 
					                    AND STRFTIME('%Y', e.Date) = '{request.Year}' 
					                    {monthConstraint}
                                        AND e.Archived = 0
                                        AND (SELECT Id FROM ExpenseCategories WHERE Id = e.CategoryId AND Archived = 0) = e.CategoryId
                            WHERE	
                                u.Email = @userEmailId
	                            AND et.Archived = 0
                            GROUP BY
	                            et.Name";

                var expenses = await _dbConnection.QueryAsync<TypeStatisticsDto>(
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