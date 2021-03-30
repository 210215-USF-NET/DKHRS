using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubBL
{
    public interface IFollowBL
    {
        Task<Follow> AddFollowAsync(Follow follow);

        Task<Follow> DeleteFollowAsync(Follow follow);

        Task<List<Follow>> GetAllFollowsAsync();

        Task<List<Follow>> GetUserFollowsAsync(string useremail);

        Task<Follow> GetSingleUserFollowAsync(string useremail, string followemail);

        Task<List<Charity>> GetFollowingUserSubscriptions(string followingemail);
    }
}
