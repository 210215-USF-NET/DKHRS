using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public class SearchHistoryRepoDB :ISearchHistoryRepo
    {
        private readonly GHDBContext _context;
        public SearchHistoryRepoDB(GHDBContext context)
        {
            _context = context;
        }

        public async Task<SearchHistory> AddSearchHistoryAsync(SearchHistory newSearch)
        {
            await _context.SearchHistories.AddAsync(newSearch);
            await _context.SaveChangesAsync();
            return newSearch;
        }
        public async Task<SearchHistory> DeleteSearchHistoryAsync(SearchHistory search2BDeleted)
        {
            _context.SearchHistories.Remove(search2BDeleted);
            await _context.SaveChangesAsync();
            return search2BDeleted;
        }
        public async Task<List<SearchHistory>> GetSearchHistoriesAsync()
        {
            return await _context.SearchHistories
                .Select(sh => sh)
                .ToListAsync();
        }
        public async Task<List<SearchHistory>> GetSearchHistoriesByUserAsync(User user)
        {
            return await _context.SearchHistories.Select(sh => sh).Where(sh => sh.User == user).ToListAsync();
        }
        public async Task<SearchHistory> UpdateSearchHistoryAsync(SearchHistory search2BUpdated)
        {
            SearchHistory oldsh = await _context.SearchHistories.FindAsync(search2BUpdated.Id);
            _context.Entry(oldsh).CurrentValues.SetValues(search2BUpdated);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return search2BUpdated;
        }
    }
}
