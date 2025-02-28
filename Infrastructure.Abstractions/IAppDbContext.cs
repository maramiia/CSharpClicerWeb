using CSharpClicker.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Infrastructure.Abstractions;

public interface IAppDbContext
{
    DbSet<ApplicationRole> ApplicationRoles { get; }

    DbSet<ApplicationUser> ApplicationUsers { get; }

    DbSet<Boost> Boosts { get; }

    DbSet<UserBoost> UserBoosts { get; }
    DbSet<UserWeapons> UserWeapons { get; }
    DbSet<UserArmors> UserArmors { get; }
    DbSet<UserBosses> UserBosses { get; }

    DbSet<Boss> Bosses { get; }

    DbSet<Armor> Armors { get; }
    DbSet<Weapon> Weapons { get; }
    DbSet<UserBossImage> UserBossImages { get; }
    

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
