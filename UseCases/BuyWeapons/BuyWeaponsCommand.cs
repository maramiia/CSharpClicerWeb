using CSharpClicker.Web.UseCases.Common;
using MediatR;

namespace CSharpClicker.Web.UseCases.BuyWeapons
{
    public record BuyWeaponCommand(int WeaponId) : IRequest<ScoreWeaponDto>;
}
