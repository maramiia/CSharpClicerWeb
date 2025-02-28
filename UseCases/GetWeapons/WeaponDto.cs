namespace CSharpClicker.Web.UseCases.GetWeapons
{
    public class WeaponDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public long Price { get; init; }
        public byte[] Image { get; init; }
        public long Damage { get; init; }
        public long Quantity { get; init; }
    }
}
