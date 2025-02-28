using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Web.Domain;

public class ApplicationUser : IdentityUser<Guid>
{
    public long CurrentScore { get; set; }

    public long RecordScore { get; set; }
    public long Power { get; set; }
    public long Protection { get; set; }

    public ICollection<UserBoost> UserBoosts { get; set; } = [];

    public ICollection<UserBosses> UserBosses { get; set; } = [];
    public ICollection<UserArmors> UserArmors { get; set; } = [];
    public ICollection<UserWeapons> UserWeapons { get; set; } = [];
    public ICollection<UserBossImage> UserBossImages { get; set; } = [];

}
