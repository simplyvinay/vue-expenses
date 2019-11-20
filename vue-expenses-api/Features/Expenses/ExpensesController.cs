using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;

namespace vue_expenses_api.Features.Expenses
{
    [Route("expenses")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpensesController : Controller
    {
        private readonly IMediator _mediator;

        public ExpensesController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ExpenseDto>> Get()
        {
            return await _mediator.Send(new ExpenseList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ExpenseDto> Get(
            int id)
        {
            return await _mediator.Send(new ExpenseDetails.Query(id));
        }

        [HttpPost]
        public async Task<ExpenseDto> Create(
            [FromBody] ExpenseCreate.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ExpenseDto> Update(
            int? id,
            [FromBody] ExpenseUpdate.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task Delete(
            int id)
        {
            await _mediator.Send(new ExpenseDelete.Command(id));
        }
    }
}