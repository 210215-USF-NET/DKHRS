using GivHubModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivHubBL
{
    public interface ICharityBL
    {
        Task<Charity> AddCharityAsync(Charity newCharity);
        Task<Charity> DeleteCharityAsync(Charity charity2BDeleted);
        Task<List<Charity>> GetCharitiesAsync();
        Task<List<Charity>> GetCharitiesByCategoryAsync(string category);
        Task<Charity> GetCharityByIdAsync(int id);

        Task<Charity> GetCharityByEidAsync(string eid);

        Task<Charity> GetCharityByNameAsync(string name);
        Task<Charity> GetCharityByWebsiteAsync(string website);

        Task<List<Charity>> GetPopularCharitiesAsync();
        Task<Charity> UpdateCharityAsync(Charity charity2BUpdated);
    }
}