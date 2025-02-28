using CSharpClicker.Web.Domain;
using CSharpClicker.Web.DomainServices;
using CSharpClicker.Web.Infrastructure.Abstractions;
using CSharpClicker.Web.UseCases.BuyWeapons;
using CSharpClicker.Web.UseCases.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.UseCases.BuyWeapon
{
    public class BuyWeaponCommandHandler : IRequestHandler<BuyWeaponCommand, ScoreWeaponDto>
    {
        private readonly ICurrentUserAccessor currentUserAccessor;
        private readonly IAppDbContext appDbContext;

        public BuyWeaponCommandHandler(ICurrentUserAccessor currentUserAccessor, IAppDbContext appDbContext)
        {
            this.currentUserAccessor = currentUserAccessor;
            this.appDbContext = appDbContext;
        }

        public async Task<ScoreWeaponDto> Handle(BuyWeaponCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserAccessor.GetCurrentUserId();
            var user = await appDbContext.ApplicationUsers
                .Include(u => u.UserWeapons)
                .ThenInclude(ub => ub.Weapon)
                .FirstAsync(u => u.Id == userId, cancellationToken);
            var weapon = await appDbContext.Weapons
                .FirstOrDefaultAsync(w => w.Id == request.WeaponId, cancellationToken);


            var existingUserWeapon = user.UserWeapons.FirstOrDefault(ub => ub.WeaponId == request.WeaponId);

            var price = 0L;

            UserWeapons userWeapon = existingUserWeapon!;
            if (existingUserWeapon != null)
            {
                price = existingUserWeapon.CurrentPrice;
                existingUserWeapon.Quantity++;
                existingUserWeapon.CurrentPrice = Convert.ToInt64(existingUserWeapon.CurrentPrice * DomainConstants.BoostCostModifier);
            }
            else
            {
                price = weapon.Price;
                var newUserWeapon = new UserWeapons()
                {
                    Weapon = weapon,
                    CurrentPrice = Convert.ToInt64(weapon.Price * DomainConstants.BoostCostModifier),
                    Quantity = 1,
                    User = user,
                };

                userWeapon = newUserWeapon;
                await appDbContext.UserWeapons.AddAsync(newUserWeapon, cancellationToken);
            }

            if (price > user.CurrentScore)
            {
                throw new InvalidOperationException("Not enough score to buy a boost.");
            }

            user.CurrentScore -= price;
            user.Power = user.UserWeapons.GetProfit();


            await appDbContext.SaveChangesAsync(cancellationToken);

            return new ScoreWeaponDto
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
                Price = userWeapon.CurrentPrice,
                Quantity = userWeapon.Quantity,
            };

            
        }
    }
}