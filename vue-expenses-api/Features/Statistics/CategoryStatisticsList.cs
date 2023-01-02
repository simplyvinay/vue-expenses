using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Statistics;

public class CategoryStatisticsList
{
    public class Query : IRequest<List<CategoryStatisticsDto>>
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
            var monthConstraint = request.Month.HasValue
                ? $" AND CAST (STRFTIME('%m', e.Date) AS INT) = {request.Month.Value} "
                : string.Empty;

            var budgetSelector = request.Month.HasValue
                ? "ec.Budget AS Budget"
                : "ec.Budget * 12 AS Budget";

            var sql = $@"SELECT 
	                            ec.Id AS Id,
	                            ec.Name AS Name,
	                            {budgetSelector},
	                            ec.ColourHex AS Colour,
	                            CAST(SUM(COALESCE(e.Value, 0)) AS REAL) AS Spent
                            FROM 
	                            ExpenseCategories ec
                            INNER JOIN
	                            Users u ON u.Id = ec.UserId
                            LEFT JOIN
	                            Expenses e ON e.CategoryId = ec.Id 
					                    AND STRFTIME('%Y', e.Date) = '{request.Year}' 
					                    {monthConstraint}
                                        AND e.Archived = 0
                                        AND (SELECT Id FROM ExpenseTypes WHERE Id = e.TypeId AND Archived = 0) = e.TypeId
                            WHERE	
                                u.Email = @userEmailId
	                            AND ec.Archived = 0
                            GROUP BY
	                            ec.Name";

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