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
    public class CategoryStatisticsList
    {
        public class Query : IRequest<List<CategoryStatisticsDto>>
        {
        }

        public class QueryHandler : IRequestHandler<Query, List<CategoryStatisticsDto>>
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

            public async Task<List<CategoryStatisticsDto>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                var sql = $@"SELECT 
	                            ec.Id,
	                            ec.Name AS Name,
	                            ec.Budget AS Budget,
	                            ec.ColourHex AS Colour,
	                            COALESCE(SUM(e.Value), 0) AS Spent,
                                STRFTIME('%m', e.Date) as Month
                            FROM 
	                            ExpenseCategories ec
                            LEFT JOIN	
	                            Expenses e ON ec.Id = e.CategoryId 
                                    AND e.Archived = 0
                                    AND STRFTIME('%Y', e.Date) = STRFTIME('%Y', DATE('now'))
                                    AND STRFTIME('%m', e.Date) <= STRFTIME('%m', DATE('now'))
                            INNER JOIN
                                Users u ON u.Id = ec.UserId
                            WHERE 
                                u.Email = @userEmailId 
                                AND ec.Archived = 0
                            GROUP BY 
	                            ec.Name,
	                            STRFTIME('%m', e.Date)
                            ORDER BY 
	                            Month";

                var expenses = await _dbConnection.QueryAsync<CategoryStatisticsDto>(
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