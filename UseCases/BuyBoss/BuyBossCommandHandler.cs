using CSharpClicker.Web.Domain;
using CSharpClicker.Web.DomainServices;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.BuyBoss;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;


public class BuyBossCommandHandler : IRequestHandler<BuyBossCommand, ScoreBossDto>
{
    private readonly ICurrentUserAccessor currentUserAccessor;
    private readonly IAppDbContext appDbContext;

    public BuyBossCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
    {
        this.currentUserAccessor = currentUserAccessor;
        this.appDbContext = appDbContext;
    }

    public async Task<ScoreBossDto> Handle(BuyBossCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserAccessor.GetCurrentUserId();
        var user = await appDbContext.ApplicationUsers
            .Include(u => u.UserBosses)
            .Include(u => u.UserBossImages)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
            throw new InvalidOperationException("User not found.");

        var boss = await appDbContext.Bosses.FirstOrDefaultAsync(b => b.Id == request.BossId, cancellationToken);
        if (boss == null)
            throw new ArgumentException("Invalid BossId.");

        var userBoss = user.UserBosses.FirstOrDefault(ub => ub.BossId == request.BossId);

        var totalStats = user.Power + user.Protection;
        if (totalStats < boss.Price)
            throw new InvalidOperationException("Not enough power and protection to buy this boss.");

        if (userBoss != null)
        {
            userBoss.Quantity++;
            userBoss.CurrentPrice = Convert.ToInt64(userBoss.CurrentPrice * DomainConstants.BoostCostModifier);
        }
        else
        {
            userBoss = new UserBosses
            {
                BossId = boss.Id,
                UserId = userId,
                IsBuy = true, 
                CurrentPrice = Convert.ToInt64(boss.Price * DomainConstants.BoostCostModifier),
                Quantity = 1
            };
            await appDbContext.UserBosses.AddAsync(userBoss, cancellationToken);
        }

        var bossImage = new UserBossImage
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BossId = boss.Id,
            Image = boss.Image
        };
        await appDbContext.UserBossImages.AddAsync(bossImage, cancellationToken);

        user.CurrentScore += boss.Reward;
        await appDbContext.SaveChangesAsync(cancellationToken);

        return new ScoreBossDto
        {
            Score = new ScoreDto
            {
                CurrentScore = user.CurrentScore,
                RecordScore = user.RecordScore,
                ProfitPerClick = user.UserBoosts.GetProfit(),
                ProfitPerSecond = user.UserBoosts.GetProfit(shouldCalculateAutoBoosts: true),
                Power = user.Power,
                Protection = user.Protection
            },
            Price = userBoss.CurrentPrice,
            Quantity = userBoss.Quantity,
            ImageBoss = Convert.ToBase64String(boss.Image),
            IsBuy = true
        };
    }
}
