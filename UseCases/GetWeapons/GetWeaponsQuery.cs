using CSharpClicker.Web.UseCases.GetWeapons;
using MediatR;

namespace CSharpClicker.Web.UseCases.GetWeapons
{
    public record GetWeaponsQuery : IRequest<IReadOnlyCollection<WeaponDto>>;
}
