using CSharpClicker.Web.Domain;
using System.Diagnostics.SymbolStore;

namespace CSharpClicker.Web.DomainServices;

public static class BoostsProfitCalculationExtensions
{
    public static long GetProfit(this IEnumerable<UserBoost> userBoosts, bool shouldCalculateAutoBoosts = false)
    {
        if (shouldCalculateAutoBoosts)
        {
            return userBoosts
                .Where(ub => ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);
        }

        var sum = 1 + userBoosts
                .Where(ub => !ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);

        if (sum == 379)
        {
            var d = 10;
        }

        return 1 + userBoosts
                .Where(ub => !ub.Boost.IsAuto)
                .Sum(ub => ub.Quantity * ub.Boost.Profit);
    }
}
