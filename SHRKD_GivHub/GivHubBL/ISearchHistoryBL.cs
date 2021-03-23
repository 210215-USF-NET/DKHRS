using GivHubModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivHubBL
{
    public interface ISearchHistoryBL
    {
        Task<SearchHistory> AddSearchHistoryAsync(SearchHistory newSearch);
        Task<SearchHistory> DeleteSearchHistoryAsync(SearchHistory search2BDeleted);
        Task<List<SearchHistory>> GetSearchHistoriesAsync();
        Task<List<SearchHistory>> GetSearchHistoriesByUserAsync(User user);
        Task<SearchHistory> UpdateSearchHistoryAsync(SearchHistory search2BUpdated);
    }
}