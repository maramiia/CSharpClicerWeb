namespace CSharpClicker.Web.Domain
{
    public class Weapon
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public long Price { get; set; }

        public long Damage { get; set; }

        public byte[] Image { get; set; }
    }
}
