using GivHubModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivHubBL
{
    public interface IDonationBL
    {
        Task<Donation> AddDonationAsync(Donation newDonation);
        Task<Donation> DeleteDonationAsync(Donation donation2BDeleted);
        Task<Donation> GetDonationByIdAsync(int id);
        Task<List<Donation>> GetDonationsAsync();
        Task<List<Donation>> GetDonationsByCharityAsync(Charity charity);
        Task<List<Donation>> GetDonationsByUserAsync(string email);
        Task<Donation> UpdateDonationAsync(Donation donation2BUpdated);
    }
}