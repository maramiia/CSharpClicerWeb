namespace CSharpClicker.Web.Domain
{
    public class Boss
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public long Price { get; set; }

        public long Reward { get; set; }

        public byte[] Image { get; set; }

        public bool IsBuy { get; set; }
        
    }
}
