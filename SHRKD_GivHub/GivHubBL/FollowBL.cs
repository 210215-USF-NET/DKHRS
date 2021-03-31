using GivHubDL;
using GivHubModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubBL
{
    public class FollowBL : IFollowBL
    {
        private IFollowRepo _repo;
        public FollowBL(IFollowRepo repo)
        {
            _repo = repo;
        }

        public async Task<Follow> AddFollowAsync(Follow follow)
        {
            return await _repo.AddFollowAsync(follow);
        }

        public async Task<Follow> DeleteFollowAsync(Follow follow)
        {
            return await _repo.DeleteFollowAsync(follow);
        }

        public async Task<List<Follow>> GetAllFollowsAsync()
        {
            return await _repo.GetAllFollowsAsync();
        }

        public async Task<List<Follow>> GetUserFollowsAsync(string useremail)
        {
            return await _repo.GetUserFollowsAsync(useremail);
        }

        public async Task<Follow> GetSingleUserFollowAsync(string useremail, string followemail)
        {
            return await _repo.GetSingleUserFollowAsync(useremail, followemail);
        }

        public async Task<List<Charity>> GetFollowingUserSubscriptions(string followingemail)
        {
            return await _repo.GetFollowingUserSubscriptions(followingemail);
        }
    }
}
