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
        public enum EnumCategoryBreakdownBy
        {
            Month,
            Year
        }

        public class Query : IRequest<List<CategoryStatisticsDto>>
        {
            public Query(
                EnumCategoryBreakdownBy categoryBreakdownBy)
            {
                CategoryBreakdownBy = categoryBreakdownBy;
            }

            public EnumCategoryBreakdownBy CategoryBreakdownBy { get; set; }
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
                var yearMonthCriteria = request.CategoryBreakdownBy == EnumCategoryBreakdownBy.Month
                    ? @" AND STRFTIME('%m', e.Date) = STRFTIME('%m', DATE('now'))
	                     AND STRFTIME('%Y', e.Date) = STRFTIME('%Y', DATE('now'))"
                    : " AND STRFTIME('%Y', e.Date) = STRFTIME('%Y', DATE('now'))";

                var sql = $@"SELECT 
	                            ec.Id,
	                            ec.Name AS Name,
	                            ec.Budget AS Budget,
	                            ec.ColourHex AS Colour,
	                            COALESCE(SUM(e.Value), 0) AS Spent	
                            FROM 
	                            ExpenseCategories ec
                            INNER JOIN	
	                            Expenses e ON ec.Id = e.CategoryId
                            INNER JOIN
                                Users u ON u.Id = ec.UserId
                            WHERE 
                                u.Email=@userEmailId
	                            {yearMonthCriteria}";

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