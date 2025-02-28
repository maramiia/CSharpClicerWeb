using CSharpClicker.Web.Domain;
using CSharpClicker.Web.UseCases.GetCurrentUser;

public class UserDto
{
    public string UserName { get; init; }

    public long CurrentScore { get; init; }

    public long RecordScore { get; init; }
    public long Power { get; set; }
    public long Protection { get; set; }

    public IReadOnlyCollection<UserBoostDto> UserBoosts { get; init; }
    public IReadOnlyCollection<UserWeaponsDto> UserWeapons { get; init; }
    public IReadOnlyCollection<UserArmorsDto> UserArmors { get; init; }
    public IReadOnlyCollection<UserBossesDto> UserBosses { get; init; }
    public long ProfitPerClick { get; set; }

    public long ProfitPerSecond { get; set; }

}
