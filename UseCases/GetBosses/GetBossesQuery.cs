using CSharpClicker.Web.UseCases.GetBooses;
using MediatR;

namespace CSharpClicker.Web.UseCases.GetBosses;

public record GetBossesQuery : IRequest<IReadOnlyCollection<BossDto>>;
