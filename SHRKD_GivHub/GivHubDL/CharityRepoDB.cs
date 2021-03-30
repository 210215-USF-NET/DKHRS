using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GivHubDL
{
    public class CharityRepoDB :ICharityRepo
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
                .Include("Location")
                .AsNoTracking()
                .Select(charities => charities)
                .ToListAsync();
        }
        public async Task<List<Charity>> GetCharitiesByCategoryAsync(string category)
        {
            return await _context.Charities.
                Include("Location")
                .AsNoTracking().
                Select(O => O).
                Where(O => O.Category == category).
                ToListAsync();

        }
        public async Task<Charity> GetCharityByIdAsync(int id)
        {
            return await _context.Charities.
                Include("Location")
                .AsNoTracking().
                FirstOrDefaultAsync(charity => charity.Id == id);
        }

        public async Task<Charity> GetCharityByEidAsync(string eid)
        {
            return await _context.Charities.
                Include("Location")
                .AsNoTracking().
                FirstOrDefaultAsync(charity => charity.EID == eid);
        }
        public async Task<Charity> GetCharityByNameAsync(string name)
        {
            return await _context.Charities.
                Include("Location")
                .AsNoTracking().
                FirstOrDefaultAsync(charity => charity.Name == name);
        }
        public async Task<Charity> GetCharityByWebsiteAsync(string website)
        {
            website = Regex.Replace(website, "%2F", "/");
            website = Regex.Replace(website,"%3A", ":");
            return await _context.Charities.
                Include("Location")
                .AsNoTracking().
                FirstOrDefaultAsync(charity => charity.Website == website);
        }

        public async Task<List<Charity>> GetPopularCharitiesAsync()
        {
            var popchar =
            from charity in _context.Charities
            join sub in _context.Subscriptions
            on charity.EID equals sub.CharityId.ToString()
            join loc in _context.Locations
            on charity.Location equals loc
            where charity.EID == sub.CharityId.ToString()
            select new
            {
                charity.Location,
                loc,
                charity
            };
            popchar = popchar.Distinct();
            foreach (var pc in popchar)
            {
                pc.charity.Location = pc.loc;
            }

            return await popchar.Select(x => x.charity).ToListAsync();
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
