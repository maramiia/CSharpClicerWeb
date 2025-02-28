using CSharpClicker.Web.Domain;

namespace CSharpClicker.Web.DomainServices
{
    public static class WeaponsProfitCalculationExtensions
    {
        public static long GetProfit(this IEnumerable<UserWeapons> userWeapons)
        {
            return userWeapons
                 .Sum(ub => ub.Quantity *  ub.Weapon.Damage);

        }
    }
}


