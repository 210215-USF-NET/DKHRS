using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public interface ISubscriptionRepo
    {
        Task<Subscription> AddSubscriptionAsync(Subscription newSub);
        Task<Subscription> DeleteSubscriptionAsync(Subscription sub2BDeleted);
        Task<Subscription> GetSingleUserSubscription(string email, int charityval);
        Task<List<Subscription>> GetSubscriptionsAsync();
        Task<List<Subscription>> GetSubscriptionsByUserAsync(string email);
        Task<Subscription> UpdateSubscriptionAsync(Subscription sub2BUpdated);
    }
}
