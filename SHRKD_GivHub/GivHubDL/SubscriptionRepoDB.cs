using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public class SubscriptionRepoDB
    {
        private readonly GHDBContext _context;
        public SubscriptionRepoDB(GHDBContext context)
        {
            _context = context;
        }

        public async Task<Subscription> AddSubscriptionAsync(Subscription newSub)
        {
            await _context.Subscriptions.AddAsync(newSub);
            await _context.SaveChangesAsync();
            return newSub;
        }
        public async Task<Subscription> DeleteSubscriptionAsync(Subscription sub2BDeleted)
        {
            _context.Subscriptions.Remove(sub2BDeleted);
            await _context.SaveChangesAsync();
            return sub2BDeleted;
        }
        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return await _context.Subscriptions
                .Select(subs => subs)
                .ToListAsync();
        }
        public async Task<List<Subscription>> GetSubscriptionsByUserAsync(string email)
        {
            return await _context.Subscriptions.Select(subs => subs).Where(subs => subs.Email == email).ToListAsync();
        }
        public async Task<Subscription> UpdateSubscriptionAsync(Subscription sub2BUpdated)
        {
            Subscription oldsub = await _context.Subscriptions.FindAsync(sub2BUpdated.Id);
            _context.Entry(oldsub).CurrentValues.SetValues(sub2BUpdated);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            return sub2BUpdated;
        }
    }
}
