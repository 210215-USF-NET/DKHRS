using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GivHubDL;
using GivHubModels;

namespace GivHubBL
{
    public class SubscriptionBL : ISubscriptionBL
    {
        private ISubscriptionRepo _repo;
        public SubscriptionBL(ISubscriptionRepo repo)
        {
            _repo = repo;
        }
        public async Task<Subscription> AddSubscriptionAsync(Subscription newSub)
        {
            return await _repo.AddSubscriptionAsync(newSub);
        }

        public async Task<Subscription> DeleteSubscriptionAsync(Subscription sub2BDeleted)
        {
            return await _repo.DeleteSubscriptionAsync(sub2BDeleted);
        }

        public async Task<Subscription> GetSingleUserSubscription(string email, int charityval)
        {
            return await _repo.GetSingleUserSubscription(email, charityval);
        }

        public async Task<List<Subscription>> GetSubscriptionsAsync()
        {
            return await _repo.GetSubscriptionsAsync();
        }

        public async Task<List<Subscription>> GetSubscriptionsByUserAsync(string email)
        {
            return await _repo.GetSubscriptionsByUserAsync(email);
        }

        public async Task<Subscription> UpdateSubscriptionAsync(Subscription sub2BUpdated)
        {
            return await _repo.UpdateSubscriptionAsync(sub2BUpdated);
        }
    }
}
