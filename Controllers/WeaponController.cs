using CSharpClicker.Web.UseCases.BuyArmor;
using CSharpClicker.Web.UseCases.BuyWeapon;
using CSharpClicker.Web.UseCases.BuyWeapons;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Web.Controllers
{
    [Route("weapon")]
    public class WeaponController : ControllerBase
    {
        private readonly IMediator mediator;

        public WeaponController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] BuyWeaponCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);     
        }
    }
}