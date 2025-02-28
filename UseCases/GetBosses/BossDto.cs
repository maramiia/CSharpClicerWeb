namespace CSharpClicker.Web.UseCases.GetBooses
{
    public class BossDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public long Price { get; init; }
        public long Reward { get; init; }
        public long UnlockCost { get; init; }
        public byte[] Image { get; init; }
        public bool IsBuy { get; init; }
    }
}
