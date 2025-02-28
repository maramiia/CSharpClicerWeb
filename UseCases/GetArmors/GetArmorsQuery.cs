using CSharpClicker.Web.UseCases.GetBooses;
using MediatR;

namespace CSharpClicker.Web.UseCases.GetArmors
{
    public record GetArmorsQuery : IRequest<IReadOnlyCollection<ArmorDto>>;
}
