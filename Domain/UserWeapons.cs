namespace CSharpClicker.Web.Domain
{
    public class UserWeapons
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int WeaponId { get; set; }

        public Weapon Weapon { get; set; }

        public long CurrentPrice { get; set; }
        public long Damage { get; set; }

        public int Quantity { get; set; }
    }
}
