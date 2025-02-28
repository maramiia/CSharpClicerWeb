namespace CSharpClicker.Web.UseCases.Common;

public record ScoreDto
{
    public long CurrentScore { get; init; }

    public long RecordScore { get; init; }

    public long Power { get; init; }
    public long Protection { get; init; }
    public long ProfitPerClick { get; init; }

    public long ProfitPerSecond { get; init; }

    

}
