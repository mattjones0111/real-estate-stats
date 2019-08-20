using System.Collections.Generic;
using System.Threading.Tasks;
using FundaApiClient.Model;

namespace FundaApiClient
{
    public interface IFetchProperties
    {
        Task<IEnumerable<Property>> GetPropertiesAsync(string searchText);
    }
}