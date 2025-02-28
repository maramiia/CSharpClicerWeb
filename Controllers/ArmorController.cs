using CSharpClicker.Web.UseCases.BuyArmor;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace CSharpClicker.Web.Controllers;

[Route("armor")]
public class ArmorController : ControllerBase
{
    private readonly IMediator mediator;

    public ArmorController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("buy")]
    public async Task<IActionResult> Buy([FromBody] BuyArmorCommand command)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
;
            return StatusCode(500, "Произошла внутренняя ошибка сервера.");
        }
    }


}