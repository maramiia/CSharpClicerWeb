using CSharpClicker.Web.Domain;
using CSharpClicker.Web.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Web.Infrastructure.DataAccess;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IAppDbContext
{
    public DbSet<ApplicationRole> ApplicationRoles { get; private set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; private set; }
    public DbSet<Boost> Boosts { get; private set; }
    
    public DbSet<Boss> Bosses { get; set; }
    public DbSet<Armor> Armors { get; set; }
    public DbSet<Weapon> Weapons { get; set; }

    public DbSet<UserBoost> UserBoosts { get; private set; }
    
    public DbSet<UserBosses> UserBosses { get; private set; }
    public DbSet<UserArmors> UserArmors { get; private set; }
    public DbSet<UserWeapons> UserWeapons { get; private set; }
    public DbSet<UserBossImage> UserBossImages { get; private set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserBoost>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBoosts)
            .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserBoost>()
            .HasOne(ub => ub.Boost)
            .WithMany()
            .HasForeignKey(ub => ub.BoostId);
        builder.Entity<UserBoost>()
           .HasOne(ub => ub.User)
           .WithMany(u => u.UserBoosts)
           .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserBosses>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBosses)
            .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserBosses>()
            .HasOne(ub => ub.Boss)
            .WithMany()
            .HasForeignKey(ub => ub.BossId);
        builder.Entity<UserBosses>()
           .HasOne(ub => ub.User)
           .WithMany(u => u.UserBosses)
           .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserArmors>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserArmors)
            .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserArmors>()
            .HasOne(ub => ub.Armor)
            .WithMany()
            .HasForeignKey(ub => ub.ArmorId);
        builder.Entity<UserArmors>()
           .HasOne(ub => ub.User)
           .WithMany(u => u.UserArmors)
           .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserWeapons>()
           .HasOne(ub => ub.User)
           .WithMany(u => u.UserWeapons)
           .HasForeignKey(ub => ub.UserId);

        builder.Entity<UserWeapons>()
            .HasOne(ub => ub.Weapon)
            .WithMany()
            .HasForeignKey(ub => ub.WeaponId);
        builder.Entity<UserWeapons>()
           .HasOne(ub => ub.User)
           .WithMany(u => u.UserWeapons)
           .HasForeignKey(ub => ub.UserId);


    }
}
