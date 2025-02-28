namespace CSharpClicker.Web.UseCases.GetArmors
{
    public class ArmorDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public long Price { get; init; }
        public byte[] Image { get; init; }
        public long Profit { get; init; }
        
    }
}
