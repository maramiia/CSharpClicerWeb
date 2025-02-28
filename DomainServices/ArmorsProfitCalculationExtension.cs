using CSharpClicker.Web.Domain;

namespace CSharpClicker.Web.DomainServices
{
    public static class ArmorsProfitCalculationExtension
    {
        public static long GetProfit(this IEnumerable<UserArmors> userArmors)
        {
           
           return userArmors
                 .Sum(ub => ub.Quantity *  ub.Armor.Profit);
        }
    }
}
