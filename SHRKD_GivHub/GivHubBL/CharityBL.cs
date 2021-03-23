using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GivHubDL;
using GivHubModels;

namespace GivHubBL
{
    public class CharityBL : ICharityBL
    {
        private ICharityRepository _repo;
        public CharityBL(ICharityRepository repo)
        {
            _repo = repo;
        }

        public async Task<Charity> AddCharityAsync(Charity newCharity)
        {
            return await _repo.AddCharityAsync(newCharity);
        }

        public async Task<Charity> DeleteCharityAsync(Charity charity2BDeleted)
        {
            return await _repo.DeleteCharityAsync(charity2BDeleted);
        }

        public async Task<List<Charity>> GetCharitiesAsync()
        {
            return await _repo.GetCharitiesAsync();
        }

        public async Task<List<Charity>> GetCharitiesByCategoryAsync(string category)
        {
            return await _repo.GetCharitiesByCategoryAsync(category);
        }

        public async Task<Charity> GetCharityByIdAsync(int id)
        {
            return await _repo.GetCharityByIdAsync(id);
        }

        public async Task<Charity> GetCharityByNameAsync(string name)
        {
            return await _repo.GetCharityByNameAsync(name);
        }

        public async Task<Charity> GetCharityByWebsiteAsync(string website)
        {
            return await _repo.GetCharityByWebsiteAsync(website);
        }

        public async Task<Charity> UpdateCharityAsync(Charity charity2BUpdated)
        {
            return await _repo.UpdateCharityAsync(charity2BUpdated);
        }
    }
}