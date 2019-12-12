using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;

namespace vue_expenses_api.Features.ExpenseTypes
{
    [Route("expensetypes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ExpenseTypeController : Controller
    {
        private readonly IMediator _mediator;

        public ExpenseTypeController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ExpenseTypeDto>> Get()
        {
            return await _mediator.Send(new ExpenseTypeList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ExpenseTypeDto> Get(
            int id)
        {
            return await _mediator.Send(new ExpenseTypeDetails.Query(id));
        }

        [HttpPost]
        public async Task<ExpenseTypeDto> Create(
            [FromBody] CreateExpenseType.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ExpenseTypeDto> Update(
            int? id,
            [FromBody] UpdateExpenseType.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task Delete(
            int id)
        {
            await _mediator.Send(new DeleteExpenseType.Command(id));
        }
    }
}