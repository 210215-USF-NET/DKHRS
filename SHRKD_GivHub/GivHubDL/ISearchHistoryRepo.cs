﻿using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    interface ISearchHistoryRepo
    {
        Task<SearchHistory> AddSearchHistoryAsync(SearchHistory newSearch);
        Task<SearchHistory> DeleteSearchHistoryAsync(SearchHistory search2BDeleted);
        Task<List<SearchHistory>> GetSearchHistoriesAsync();
        Task<List<SearchHistory>> GetSearchHistoriesByUserAsync(User user);
        Task<SearchHistory> UpdateSearchHistoryAsync(SearchHistory search2BUpdated);
    }
}
