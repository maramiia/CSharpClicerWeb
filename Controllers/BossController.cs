using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.Infrastructure.DataAccess;
using CSharpClicker.Web.Infrastructure.Implementations;
using CSharpClicker.Web.UseCases.BuyBoss;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Controllers
{
    [Route("boss")]
    public class BossController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICurrentUserAccessor currentUserAccessor;
        private readonly IAppDbContext appDbContext;

        public BossController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> Buy([FromBody] BuyBossCommand command)
        {     
             var result = await mediator.Send(command);
             return Ok(result);   
        }

    }
}

