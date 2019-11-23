using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;
using vue_expenses_api.Features.ExpenseCategories;
using vue_expenses_api.Features.Expenses;

namespace vue_expenses_api.Features.Statistics
{
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
                new CategoryStatisticsList.Query());
        }

        [HttpGet]
        [Route("getcurrentyearexpensesbycategorybreakdown")]
        public async Task<List<ExpenseByCategoryStatisticsDto>> GetCurrentYearExpensesByCategoryBreakdown()
        {
            return await _mediator.Send(new ExpenseStatisticsList.ExpensesByCategoryThisYearQuery());
        }

        [HttpGet]
        [Route("getexpenesesbycategory/{year}")]
        public async Task<List<ExpenesesByCategorySeriesDto>> GetExpenesesByCategorySeries(int year)
        {
            var expenses = await _mediator.Send(new ExpenseList.Query(year));
            var categories = await _mediator.Send(new ExpenseCategoryList.Query());
            var months = Enumerable.Range(
                1,
                12);
            var list = new List<ExpenesesByCategorySeriesDto>();

            foreach (var category in categories)
            {
                var x = new ExpenesesByCategorySeriesDto
                {
                    Name = category.Name,
                    Type = "bar",
                    Stack = "stack",
                    BarWidth = "45%",
                    Label = new {Normal = new {Show = false}},
                    ItemStyle = new {Color = category.ColourHex},
                    Data = months.GroupJoin(
                            expenses,
                            month => new
                            {
                                Month = month, Category = category.Name
                            },
                            e => new
                            {
                                e.Month, e.Category
                            },
                            (month, gp) => gp.Any() ? Math.Round(gp.Sum(expense => expense.Value), 2) : 0)
                        .ToArray()
                };
                list.Add(x);
            }

            return list;
        }

    }
}