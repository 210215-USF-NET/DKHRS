using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GivHubDL;
using GivHubModels;

namespace GivHubBL
{
    public class SearchHistoryBL : ISearchHistoryBL
    {
        private ISearchHistoryRepo _repo;
        public SearchHistoryBL(ISearchHistoryRepo repo)
        {
            _repo = repo;
        }
        public async Task<SearchHistory> AddSearchHistoryAsync(SearchHistory newSearch)
        {
            return await _repo.AddSearchHistoryAsync(newSearch);
        }

        public async Task<SearchHistory> DeleteSearchHistoryAsync(SearchHistory search2BDeleted)
        {
            return await _repo.DeleteSearchHistoryAsync(search2BDeleted);
        }

        public async Task<List<SearchHistory>> GetSearchHistoriesAsync()
        {
            return await _repo.GetSearchHistoriesAsync();
        }

        public async Task<List<SearchHistory>> GetSearchHistoriesByUserAsync(string email)
        {
            return await _repo.GetSearchHistoriesByUserAsync(email);
        }

        public async Task<SearchHistory> UpdateSearchHistoryAsync(SearchHistory search2BUpdated)
        {
            return await _repo.UpdateSearchHistoryAsync(search2BUpdated);
        }
    }
}
