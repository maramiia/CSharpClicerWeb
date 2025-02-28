namespace CSharpClicker.Web.Domain
{
    public class Armor
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public long Price { get; set; }
        public long Profit { get; set; }

        public bool IsAuto { get; set; }

        public byte[] Image { get; set; } = null!;
    }
}
