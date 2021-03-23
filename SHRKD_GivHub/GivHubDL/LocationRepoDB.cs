using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    class LocationRepoDB : ILocationRepo
    {
        private readonly GHDBContext _context;
        public LocationRepoDB(GHDBContext context)
        {
            _context = context;
        }
        public async Task<Location> AddLocationAsync(Location newLoc)
        {
            await _context.Locations.AddAsync(newLoc);
            await _context.SaveChangesAsync();
            return newLoc;
        }
        public async Task<Location> DeleteLocationAsync(Location location2BDeleted)
        {
            _context.Locations.Remove(location2BDeleted);
            await _context.SaveChangesAsync();
            return location2BDeleted;
        }
        public async Task<Location> GetLocationByCityStateAsync(string city, string state)
        {
            return await _context.Locations
                .FirstOrDefaultAsync(location => location.City == city);
        }
        public async Task<Location> GetLocationByIdAsync(int id)
        {
            return await _context.Locations
                .FirstOrDefaultAsync(location => location.Id == id);
        }
        public async Task<List<Location>> GetLocationsAsync()
        {
            return await _context.Locations
                .Select(location => location)
                .ToListAsync();
        }
        public async Task<Location> UpdateLocationAsync(Location location2BUpdated)
        {
            Location oldLoc = await _context.Locations.FindAsync(location2BUpdated.Id);
            _context.Entry(oldLoc).CurrentValues.SetValues(location2BUpdated);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return location2BUpdated;
        }
    }
}
