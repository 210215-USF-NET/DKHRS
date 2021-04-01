using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public interface IDonationRepo
    {
        Task<Donation> AddDonationAsync(Donation newDonation);
        Task<Donation> DeleteDonationAsync(Donation donation2BDeleted);
        Task<Donation> GetDonationByIdAsync(int id);
        Task<List<Donation>> GetDonationsAsync();
        Task<List<Donation>> GetDonationsByCharityAsync(int x);
        Task<List<Donation>> GetDonationsByUserAsync(string email);
        Task<Donation> UpdateDonationAsync(Donation donation2BUpdated);

        Task<List<Donation>> GetTopDonationsAsync();
    }
}
