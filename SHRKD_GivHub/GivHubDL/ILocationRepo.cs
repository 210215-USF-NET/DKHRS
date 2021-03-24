using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public interface ILocationRepo
    {
            Task<Location> AddLocationAsync(Location newLoc);
            Task<Location> DeleteLocationAsync(Location location2BDeleted);
            Task<Location> GetLocationByCityStateAsync(string city, string state);
            Task<Location> GetLocationByIdAsync(int id);
            Task<List<Location>> GetLocationsAsync();
            Task<Location> UpdateLocationAsync(Location location2BUpdated);
    }
}
