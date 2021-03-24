using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public class DonationRepoDB
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
        public async Task<List<Donation>> GetDonationsByCharityAsync(Charity charity)
        {
            return await _context.Donations.Select(donation => donation).Where(donation => donation.Charity == charity).ToListAsync();
        }
        public async Task<List<Donation>> GetDonationsByUserAsync(User user)
        {
            return await _context.Donations.Select(donations => donations).Where(donations => donations.User == user).ToListAsync();
        }
        public async Task<Donation> UpdateDonationAsync(Donation donation2BUpdated)
        {
            Donation olddonation = await _context.Donations.FindAsync(donation2BUpdated.Id);
            _context.Entry(olddonation).CurrentValues.SetValues(donation2BUpdated);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return donation2BUpdated;
        }
    }
}
