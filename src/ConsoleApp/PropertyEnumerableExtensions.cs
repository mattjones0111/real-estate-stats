using System.Collections.Generic;
using System.Linq;
using FundaApiClient.Model;

namespace ConsoleApp
{
    public static class PropertyEnumerableExtensions
    {
        public static IEnumerable<Counted<RealEstateAgent>> GetTopAgents(
            this IEnumerable<Property> properties,
            int top = 10)
        {
            return properties
                .GroupBy(x => new RealEstateAgent(x.MakelaarId, x.MakelaarNaam))
                .Select(x => new Counted<RealEstateAgent>(x.Key, x.Count()))
                .OrderByDescending(x => x.Count)
                .Take(top);
        }
    }
}
