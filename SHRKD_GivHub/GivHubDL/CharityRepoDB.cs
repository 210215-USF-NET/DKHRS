using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    class CharityRepoDB :ICharityRepo
    {
        private readonly GHDBContext _context;
        public CharityRepoDB(GHDBContext context)
        {
            _context = context;
        }
        public async Task<Charity> AddCharityAsync(Charity newCharity)
        {
            await _context.Charities.AddAsync(newCharity);
            await _context.SaveChangesAsync();
            return newCharity;
        }
        public async Task<Charity> DeleteCharityAsync(Charity charity2BDeleted)
        {
            _context.Charities.Remove(charity2BDeleted);
            await _context.SaveChangesAsync();
            return charity2BDeleted;
        }
        public async Task<List<Charity>> GetCharitiesAsync()
        {
            return await _context.Charities
                .Select(charities => charities)
                .ToListAsync();
        }
        public async Task<List<Charity>> GetCharitiesByCategoryAsync(string category)
        {
            return await _context.Charities.Select(O => O).Where(O => O.Category == category).ToListAsync();

        }
        public async Task<Charity> GetCharityByIdAsync(int id)
        {
            return await _context.Charities.FirstOrDefaultAsync(charity => charity.Id == id);
        }
        public async Task<Charity> GetCharityByNameAsync(string name)
        {
            return await _context.Charities.FirstOrDefaultAsync(charity => charity.Name == name);
        }
        public async Task<Charity> GetCharityByWebsiteAsync(string website)
        {
            return await _context.Charities.FirstOrDefaultAsync(charity => charity.Website == website);
        }
        public async Task<Charity> UpdateCharityAsync(Charity charity2BUpdated)
        {
            Charity oldchar = await _context.Charities.FindAsync(charity2BUpdated.Id);
            _context.Entry(oldchar).CurrentValues.SetValues(charity2BUpdated);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return charity2BUpdated;
        }
    }
}
