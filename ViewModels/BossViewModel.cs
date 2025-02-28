using CSharpClicker.Web.UseCases.GetArmors;
using CSharpClicker.Web.UseCases.GetBooses;
using CSharpClicker.Web.UseCases.GetBoosts;
using CSharpClicker.Web.UseCases.GetWeapons;

namespace CSharpClicker.Web.ViewModels
{
    public class BossViewModel
    {
        public UserDto User { get; init; }
        public IReadOnlyCollection<BossDto> Bosses { get; init; }
        public IReadOnlyCollection<ArmorDto> Armors { get; init; }
    }
}
