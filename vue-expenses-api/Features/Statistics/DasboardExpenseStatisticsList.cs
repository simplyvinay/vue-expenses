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
    public class DasboardExpenseStatisticsList
    {
        public class ExpensesByCategoryThisYearQuery : IRequest<List<ExpenseByCategoryStatisticsDto>>
        {
            public ExpensesByCategoryThisYearQuery()
            {
            }
        }

        public class QueryHandler : IRequestHandler<ExpensesByCategoryThisYearQuery, List<ExpenseByCategoryStatisticsDto>>
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

            public async Task<List<ExpenseByCategoryStatisticsDto>> Handle(
                ExpensesByCategoryThisYearQuery request,
                CancellationToken cancellationToken)
            {
                var sql = $@"SELECT
                                ec.Id AS Id, 
	                            ec.Name AS CategoryName,
	                            ec.ColourHex AS CategoryColour,
	                            STRFTIME('%m', e.Date) as Month,
                                COALESCE(SUM(e.Value), 0) AS Spent	
                            FROM 
                                ExpenseCategories ec
                            INNER JOIN	
                                Expenses e ON ec.Id = e.CategoryId 
                                    AND e.Archived = 0
                                    AND (SELECT Id FROM ExpenseTypes WHERE Id = e.TypeId AND Archived = 0) = e.TypeId                                
                            INNER JOIN
                                Users u ON u.Id = ec.UserId
                            WHERE 
                                u.Email = @userEmailId  
                                AND STRFTIME('%Y', e.Date) = STRFTIME('%Y', DATE('now')) 
                                --AND STRFTIME('%m', e.Date) <= STRFTIME('%m', DATE('now'))
                                AND ec.Archived = 0
	                        GROUP BY 
                                ec.Name,
                                ec.ColourHex,
                                STRFTIME('%m', e.Date)
                            ORDER BY 
                                Month";

                var expenses = await _dbConnection.QueryAsync<ExpenseByCategoryStatisticsDto>(
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