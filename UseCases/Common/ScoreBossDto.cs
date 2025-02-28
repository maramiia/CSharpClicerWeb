namespace CSharpClicker.Web.UseCases.Common
{
    public class ScoreBossDto
    {
        public required ScoreDto Score { get; init; }
        public int Quantity { get; init; }
        public long Price { get; init; }
        public string ImageBoss { get; init; }
        public bool IsBuy { get; set; }
    }
}
