using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vue_expenses_api.Dtos;

namespace vue_expenses_api.Features.Users
{
    public class UsersController
    {
        private readonly IMediator _mediator;

        public UsersController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost("api/login")]
        [HttpPost("login")]
        public async Task<UserDto> Login(
            [FromBody] Login.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("register")]
        public async Task<Unit> Register(
            [FromBody] Register.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("refreshtoken")]
        public async Task<UserDto> RefreshToken(
            [FromBody] ExchangeRefreshToken.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("settings")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<UserDetailsDto> Settings(
            [FromBody] SettingsUpdate.Command command)
        {
            return await _mediator.Send(command);
        }

        
        [HttpPut("profile")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ProfileDetailsDto> Profile(
            [FromBody] ProfileUpdate.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("currencies")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<CurrencyDto> Currencies()
        {
            var currencies = new List<CurrencyDto>();
            CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture).ToList()
                .ForEach(
                    culture =>
                    {
                        try
                        {
                            var regionInfo = new RegionInfo(culture.Name);
                            var dto = new CurrencyDto(
                                regionInfo.Name,
                                regionInfo.CurrencyEnglishName);
                            if (!currencies.Contains(dto))
                                currencies.Add(dto);
                        }
                        catch (System.Exception)
                        {
                            // ignored
                        }
                    });


            return currencies.OrderBy(x => x.Name).ToList();
        }
    }
}