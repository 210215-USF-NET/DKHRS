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
        private readonly IDonationRepo _repo;

        public DonationBL(IDonationRepo repo)
        {
            _repo = repo;
        }
        public async Task<Donation> AddDonationAsync(Donation newDonation)
        {
            return await _repo.AddDonationAsync(newDonation);
        }

        public async Task<Donation> DeleteDonationAsync(Donation donation2BDeleted)
        {
            return await _repo.DeleteDonationAsync(donation2BDeleted);
        }

        public async Task<Donation> GetDonationByIdAsync(int id)
        {
            return await _repo.GetDonationByIdAsync(id);
        }


        public async Task<List<Donation>> GetDonationsAsync()
        {
            return await _repo.GetDonationsAsync();
        }

        public async Task<List<Donation>> GetDonationsByCharityAsync(int x)
        {
            return await _repo.GetDonationsByCharityAsync(x);
        }

        public async Task<List<Donation>> GetDonationsByUserAsync(string email)
        {
            return await _repo.GetDonationsByUserAsync(email);
        }

        public async Task<Donation> UpdateDonationAsync(Donation donation2BUpdated)
        {
            return await _repo.UpdateDonationAsync(donation2BUpdated);
        }
    }
}
