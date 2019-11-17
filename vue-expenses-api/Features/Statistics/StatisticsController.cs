using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;

namespace vue_expenses_api.Features.Statistics
{
    [Route("statistics")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StatisticsController
    {
        private readonly IMediator _mediator;

        public StatisticsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<CategoryStatisticsDto>> GetCurrentMonthCategoryBreakdown()
        {
            return await _mediator.Send(new StatisticsList.Query(StatisticsList.EnumCategoryBreakdownBy.Month));
        }

        [HttpGet]
        public async Task<List<CategoryStatisticsDto>> GetCurrentYearCategoryBreakdown()
        {
            return await _mediator.Send(new StatisticsList.Query(StatisticsList.EnumCategoryBreakdownBy.Year));
        }

    }
}