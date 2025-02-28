using CSharpClicker.Web.UseCases.Common;
using MediatR;

namespace CSharpClicker.Web.UseCases.BuyArmor
{
    public record BuyArmorCommand(int ArmorId) : IRequest<ScoreArmorDto>;
}
