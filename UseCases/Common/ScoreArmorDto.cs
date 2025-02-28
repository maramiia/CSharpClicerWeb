namespace CSharpClicker.Web.UseCases.Common
{
    public class ScoreArmorDto
    {
        public required ScoreDto Score { get; init; }

        public int Quantity { get; init; }

        public long Price { get; init; }
    }
}
