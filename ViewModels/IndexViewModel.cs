using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetCurrentUser;
namespace CSharpClicker.Web.ViewModels;

using CSharpClicker.Web.Domain;
using CSharpClicker.Web.UseCases.GetArmors;
using CSharpClicker.Web.UseCases.GetBooses;
using CSharpClicker.Web.UseCases.GetWeapons;

public class IndexViewModel
{
    public UserDto User { get; init; }
    public long Power { get; init; }
    public long Protection { get; init; }

    public IReadOnlyCollection<BoostDto> Boosts { get; init; }
    public IReadOnlyCollection<BossDto> Bosses { get; init; }
    public IReadOnlyCollection<UserBosses> UserBosses { get; init; }

    public IReadOnlyCollection<ArmorDto> Armors { get; init; }
    public IReadOnlyCollection<WeaponDto> Weapons { get; init; }
    public IReadOnlyCollection<UserBossImage> BossImages { get; init; }
}
