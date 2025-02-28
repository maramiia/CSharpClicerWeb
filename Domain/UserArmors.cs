namespace CSharpClicker.Web.Domain
{
    public class UserArmors
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ArmorId { get; set; }

        public Armor Armor { get; set; }

        public long CurrentPrice { get; set; }

        public int Quantity { get; set; }
    }
}
