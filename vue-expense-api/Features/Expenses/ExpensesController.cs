using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expense_api.Dtos;
using vue_expense_api.Infrastructure.Security;

namespace vue_expense_api.Features.Expenses
{
    [Route("expenses")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
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

        [HttpDelete("{id}")]
        public async Task Delete(
            int? id)
        {
            await _mediator.Send(new ExpenseDelete.Command(id));
        }
    }
}