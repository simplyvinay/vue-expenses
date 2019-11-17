using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using vue_expenses_api.Dtos;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Features.Statistics
{
    public class StatisticsList
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

            public Task<List<CategoryStatisticsDto>> Handle(
                Query request,
                CancellationToken cancellationToken)
            {
                
            }
        }
    }
}