using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;

namespace vue_expenses_api.Features.ExpenseCategories;

[Route("expensecategories")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ExpenseCategoryController : Controller
{
    private readonly IMediator _mediator;

    public ExpenseCategoryController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<ExpenseCategoryDto>> Get()
    {
        return await _mediator.Send(new ExpenseCategoryList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ExpenseCategoryDto> Get(
        int id)
    {
        return await _mediator.Send(new ExpenseCategoryDetails.Query(id));
    }

    [HttpPost]
    public async Task<ExpenseCategoryDto> Create(
        [FromBody] CreateExpenseCategory.Command command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ExpenseCategoryDto> Update(
        int id,
        [FromBody] UpdateExpenseCategory.Command command)
    {
        return await _mediator.Send(command);

    }

    [HttpDelete("{id}")]
    public async Task Delete(
        int id)
    {
        await _mediator.Send(new DeleteExpenseCategory.Command(id));
    }
}