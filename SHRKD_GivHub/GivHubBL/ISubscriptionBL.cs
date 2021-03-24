using GivHubModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GivHubBL
{
    public interface ISubscriptionBL
    {
        Task<Subscription> AddSubscriptionAsync(Subscription newSub);
        Task<Subscription> DeleteSubscriptionAsync(Subscription sub2BDeleted);
        Task<List<Subscription>> GetSubscriptionsAsync();
        Task<List<Subscription>> GetSubscriptionsByUserAsync(string email);
        Task<Subscription> UpdateSubscriptionAsync(Subscription sub2BUpdated);
    }
}