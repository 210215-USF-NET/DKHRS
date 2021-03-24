using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public interface ICharityRepo
    {
        Task<Charity> AddCharityAsync(Charity newCharity);
        Task<Charity> DeleteCharityAsync(Charity charity2BDeleted);
        Task<List<Charity>> GetCharitiesAsync();
        Task<List<Charity>> GetCharitiesByCategoryAsync(string category);
        Task<Charity> GetCharityByIdAsync(int id);
        Task<Charity> GetCharityByNameAsync(string name);
        Task<Charity> GetCharityByWebsiteAsync(string website);
        Task<Charity> UpdateCharityAsync(Charity charity2BUpdated);
    }
}
