using CSharpClicker.Web.Domain;
using CSharpClicker.Web.DomainServices;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.BuyArmor;
using CSharpClicker.Web.UseCases.BuyBoost;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.BuyArmor
{
    public class BuyArmorCommandHandler : IRequestHandler<BuyArmorCommand, ScoreArmorDto>
    {
        private readonly ICurrentUserAccessor currentUserAccessor;
        private readonly IAppDbContext appDbContext;

        public BuyArmorCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
        {
            this.currentUserAccessor = currentUserAccessor;
            this.appDbContext = appDbContext;
        }

        public async Task<ScoreArmorDto> Handle(BuyArmorCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserAccessor.GetCurrentUserId();
            var user = await appDbContext.ApplicationUsers
                .Include(u => u.UserArmors)
                .ThenInclude(ub => ub.Armor)
                .FirstAsync(u => u.Id == userId, cancellationToken);
            var armor = await appDbContext.Armors
                .FirstOrDefaultAsync(w => w.Id == request.ArmorId, cancellationToken);


            var existingUserArmor = user.UserArmors.FirstOrDefault(ub => ub.ArmorId == request.ArmorId);

            var price = 0L;

            UserArmors userArmor = existingUserArmor!;
            if (existingUserArmor != null)
            {
                price = existingUserArmor.CurrentPrice;
                existingUserArmor.Quantity++;
                existingUserArmor.CurrentPrice = Convert.ToInt64(existingUserArmor.CurrentPrice * DomainConstants.BoostCostModifier);
            }
            else
            {
                price = armor.Price;
                var newUserArmor = new UserArmors()
                {
                    Armor = armor,
                    CurrentPrice = Convert.ToInt64(armor.Price * DomainConstants.BoostCostModifier),
                    Quantity = 1,
                    User = user,
                };

                userArmor = newUserArmor;
                await appDbContext.UserArmors.AddAsync(newUserArmor, cancellationToken);
            }

            if (price > user.CurrentScore)
            {
                throw new InvalidOperationException("Not enough score to buy a boost.");
            }

            user.CurrentScore -= price;
            user.Protection = user.UserArmors.GetProfit();


            await appDbContext.SaveChangesAsync(cancellationToken);

            return new ScoreArmorDto
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
                Price = userArmor.CurrentPrice,
                Quantity = userArmor.Quantity,
            };

        }


    }
}
