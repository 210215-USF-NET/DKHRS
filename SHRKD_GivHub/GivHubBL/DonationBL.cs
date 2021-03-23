using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GivHubDL;
using GivHubModels;

namespace GivHubBL
{
    public class DonationBL : IDonationBL
    {
        private IDonationRepository _repo;

        public DonationBL(IDonationRepository repo)
        {
            _repo = repo;
        }
        public async Task<Donation> AddDonationAsync(Donation newDonation)
        {
            return await _repo.AddDonationAsync(newDonation);
        }

        public async Task<Donation> DeleteDonationAsync(Donation donation2BDeleted)
        {
            return await _repo.DeleteDontationAsync(donation2BDeleted);
        }

        public async Task<Donation> GetDonationByIdAsync(int id)
        {
            return await _repo.GetDonationByIdAsync(id);
        }

        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _repo.GetDonationsAsync();
        }

        public async Task<List<Donation>> GetDonationsByCharityAsync(Charity charity)
        {
            return await _repo.GetDonationsByCharityAsync(charity);
        }

        public async Task<List<Donation>> GetDonationsByUserAsync(User user)
        {
            return await _repo.GetDonationsByUserAsync(user);
        }

        public async Task<Donation> UpdateDonationAsync(Donation donation2BUpdated)
        {
            return await _repo.UpdateDonationAsync(donation2BUpdated);
        }
    }
}
