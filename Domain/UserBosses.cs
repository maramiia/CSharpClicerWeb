namespace CSharpClicker.Web.Domain
{
    public class UserBosses
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BossId { get; set; }

        public Boss Boss { get; set; }

        public long CurrentPrice { get; set; }

        public int Quantity { get; set; }
        public bool IsBuy { get; set; }
    }
}
