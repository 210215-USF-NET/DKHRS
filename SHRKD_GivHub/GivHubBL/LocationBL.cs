using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GivHubDL;
using GivHubModels;

namespace GivHubBL
{
    public class LocationBL : ILocationBL
    {
        private readonly ILocationRepo _repo;
        public LocationBL(ILocationRepo repo)
        {
            _repo = repo;
        }
        public async Task<Location> AddLocationAsync(Location newLoc)
        {
            return await _repo.AddLocationAsync(newLoc);
        }

        public async Task<Location> DeleteLocationAsync(Location location2BDeleted)
        {
            return await _repo.DeleteLocationAsync(location2BDeleted);
        }

        public async Task<Location> GetLocationByCityStateAsync(string city, string state)
        {
            return await _repo.GetLocationByCityStateAsync(city, state);
        }

        public async Task<Location> GetLocationByIdAsync(int id)
        {
            return await _repo.GetLocationByIdAsync(id);
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            return await _repo.GetLocationsAsync();
        }

        public async Task<Location> UpdateLocationAsync(Location location2BUpdated)
        {
            return await _repo.UpdateLocationAsync(location2BUpdated);
        }
    }
}
