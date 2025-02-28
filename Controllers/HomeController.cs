using CSharpClicker.Web.UseCases.AddPoints;
using CSharpClicker.Web.UseCases.Common;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetArmors;
using CSharpClicker.Web.UseCases.GetCurrentUser;
using CSharpClicker.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CSharpClicker.Web.UseCases.GetBosses;
using CSharpClicker.Web.UseCases.GetWeapons;

namespace CSharpClicker.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMediator mediator;

    public HomeController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var boosts = await mediator.Send(new GetBoostsQuery());
        var user = await mediator.Send(new GetCurrentUserQuery());
        var armors = await mediator.Send(new GetArmorsQuery());
        var weapons = await mediator.Send(new GetWeaponsQuery());
        var bosses = await mediator.Send(new GetBossesQuery());
        var viewModel = new IndexViewModel()
        {
            Boosts = boosts,
            User = user,
            Armors = armors,
            Weapons = weapons,
            Bosses = bosses
            
        };

        return View(viewModel);
    } 

    [HttpPost("score")]
    public async Task<ScoreDto> AddToScore(AddPointsCommand command)
        => await mediator.Send(command);

    public async Task<IActionResult> NewPage()
    {
        var user = await mediator.Send(new GetCurrentUserQuery());
        var bosses = await mediator.Send(new GetBossesQuery());
        var armors = await mediator.Send(new GetArmorsQuery());

        var bossviewmodel = new BossViewModel()
        {
            User = user,
            Bosses = bosses,
            Armors = armors
        };

        return View(bossviewmodel);
    }
    


}
