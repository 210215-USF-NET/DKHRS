using GivHubModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivHubDL
{
    public class FollowRepoDB :IFollowRepo
    {
        private readonly GHDBContext _context;
        public FollowRepoDB(GHDBContext context)
        {
            _context = context;
        }

        public async Task<Follow> AddFollowAsync(Follow follow)
        {
            Debug.WriteLine(follow.UserEmail);
            await _context.Follows.AddAsync(follow);
            await _context.SaveChangesAsync();
            return follow;
        }

        public async Task<Follow> DeleteFollowAsync(Follow follow)
        {
            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();
            return follow;
        }

        public async Task<List<Follow>> GetAllFollowsAsync()
        {
            return await _context.Follows
                .Select(follow => follow)
                .ToListAsync();
        }

        public async Task<List<Follow>> GetUserFollowsAsync(string useremail)
        {
            return await _context.Follows.Select(follows => follows).Where(follows => follows.UserEmail == useremail).ToListAsync();
        }

        public async Task<Follow> GetSingleUserFollowAsync(string useremail, string followemail)
        {
            return await _context.Follows.FirstOrDefaultAsync(follow => follow.UserEmail == useremail && follow.FollowingEmail == followemail);
        }

        public async Task<List<Charity>> GetFollowingUserSubscriptions(string followingemail)
        {
            var followsubs =
            from fs in _context.Follows
            join sub in _context.Subscriptions
            on fs.FollowingEmail equals sub.Email
            join charity in _context.Charities
            on sub.CharityId.ToString() equals charity.EID
            join loc in _context.Locations
            on charity.Location equals loc
            where fs.FollowingEmail == followingemail
            select new
            {
                charity.Location,
                loc,
                charity
            };
            followsubs = followsubs.Distinct();
            foreach (var pc in followsubs)
            {
                pc.charity.Location = pc.loc;
            }

            return await followsubs.Select(x => x.charity).ToListAsync();



        }
    }
}
