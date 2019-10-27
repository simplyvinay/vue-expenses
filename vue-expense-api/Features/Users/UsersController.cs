using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using vue_expense_api.Dtos;

namespace vue_expense_api.Features.Users
{
    public class UsersController
    {
        private readonly IMediator _mediator;

        public UsersController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<UserDto> Login(
            [FromBody] Login.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}