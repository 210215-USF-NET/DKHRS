using GivHubModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivHubBL
{
    public interface ILocationBL
    {
        Task<Location> AddLocationAsync(Location newLoc);
        Task<Location> DeleteLocationAsync(Location location2BDeleted);
        Task<Location> GetLocationByCityStateAsync(string city, string state);
        Task<Location> GetLocationByIdAsync(int id);
        Task<List<Location>> GetLocationsAsync();
        Task<Location> UpdateLocationAsync(Location location2BUpdated);
    }
}