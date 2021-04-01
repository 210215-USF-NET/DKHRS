using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public class DonationRepoDB :IDonationRepo
    {
        private readonly GHDBContext _context;
        public DonationRepoDB(GHDBContext context)
        {
            _context = context;
        }

        public async Task<Donation> AddDonationAsync(Donation newDonation)
        {
            await _context.Donations.AddAsync(newDonation);
            await _context.SaveChangesAsync();
            return newDonation;
        }
        public async Task<Donation> DeleteDonationAsync(Donation donation2BDeleted)
        {
            _context.Donations.Remove(donation2BDeleted);
            await _context.SaveChangesAsync();
            return donation2BDeleted;
        }
        public async Task<Donation> GetDonationByIdAsync(int id)
        {
            return await _context.Donations.FirstOrDefaultAsync(donation => donation.Id == id);
        }

        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _context.Donations
                .Select(donations => donations)
                .ToListAsync();
        }
        public async Task<List<Donation>> GetDonationsByCharityAsync(int x)
        {
            return await _context.Donations.Select(donation => donation).Where(donation => donation.CharityId == x).ToListAsync();
        }

        public async Task<List<Donation>> GetDonationsByUserAsync(string email)
        {
            return await _context.Donations.Select(donations => donations).Where(donations => donations.Email == email).ToListAsync();
        }
        public async Task<Donation> UpdateDonationAsync(Donation donation2BUpdated)
        {
            Donation olddonation = await _context.Donations.FindAsync(donation2BUpdated.Id);
            _context.Entry(olddonation).CurrentValues.SetValues(donation2BUpdated);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return donation2BUpdated;
        }

        public async Task<List<Donation>> GetTopDonationsAsync()
        {
            var result =
            from donation in _context.Donations
            group donation by new { donation.Email} into newgroup
            select new
            {
                Email = newgroup.Key.Email,
                Amount = newgroup.Sum(x => x.Amount)
            };
            List<Donation> newList = new List<Donation>();
            foreach (var d in result)
            {
                Donation newDonation = new Donation();
                newDonation.Email = d.Email;
                newDonation.Amount = d.Amount;
                newList.Add(newDonation);
            }
                    
            return newList;
        }
    }
}
