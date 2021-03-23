using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    interface IDonationRepo
    {
        Task<Donation> AddDonationAsync(Donation newDonation);
        Task<Donation> DeleteDonationAsync(Donation donation2BDeleted);
        Task<Donation> GetDonationByIdAsync(int id);
        Task<List<Donation>> GetDonationsAsync();
        Task<List<Donation>> GetDonationsByCharityAsync(Charity charity);
        Task<List<Donation>> GetDonationsByUserAsync(User user);
        Task<Donation> UpdateDonationAsync(Donation donation2BUpdated);
    }
}
