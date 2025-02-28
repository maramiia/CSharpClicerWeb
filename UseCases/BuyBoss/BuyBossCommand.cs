using CSharpClicker.Web.UseCases.Common;
using MediatR;

namespace CSharpClicker.Web.UseCases.BuyBoss
{
     public record BuyBossCommand(int BossId) : IRequest<ScoreBossDto>;
}
