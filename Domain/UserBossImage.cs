namespace CSharpClicker.Web.Domain
{
    public class UserBossImage
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int BossId { get; set; }
        public byte[] Image { get; set; }

        public ApplicationUser User { get; set; }
        public Boss Boss { get; set; }
    }
}
