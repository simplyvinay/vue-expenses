using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;

namespace vue_expenses_api.Features.Statistics;

[Route("statistics")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StatisticsController : Controller
{
    private readonly IMediator _mediator;

    public StatisticsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getcurrentyearcategoriesbreakdown")]
    public async Task<List<CategoryStatisticsDto>> GetCurrentYearCategoriesBreakdown()
    {
        return await _mediator.Send(
            new DashboardCategoryStatisticsList.Query());
    }

    [HttpGet]
    [Route("getcurrentyearexpensesbycategorybreakdown")]
    public async Task<List<ExpenseByCategoryStatisticsDto>> GetCurrentYearExpensesByCategoryBreakdown()
    {
        return await _mediator.Send(new DasboardExpenseStatisticsList.ExpensesByCategoryThisYearQuery());
    }

    [HttpGet]
    [Route("getcategoriesbreakdownforyear/{year}/{month?}")]
    public async Task<List<CategoryStatisticsDto>> GetCategoriesBreakdownForYear(
        int year,
        int? month = null)
    {
        return await _mediator.Send(
            new CategoryStatisticsList.Query(year, month));
    }
        
    [HttpGet]
    [Route("gettypesbreakdownforyear/{year}/{month?}")]
    public async Task<List<TypeStatisticsDto>> GetTypesBreakdownForYear(
        int year,
        int? month = null)
    {
        return await _mediator.Send(
            new TypesStatisticsList.Query(year, month));
    }
}