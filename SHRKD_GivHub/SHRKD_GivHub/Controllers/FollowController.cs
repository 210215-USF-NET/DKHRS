using GivHubBL;
using GivHubModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHRKD_GivHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowBL _folBL;


        public FollowController(IFollowBL folBL)
        {
            _folBL = folBL;
        }

        // POST api/<FollowController>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddFollowAsync([FromBody] Follow fol)
        {
            if (!(fol.UserEmail.Equals(fol.FollowingEmail)))
            {
                try
                {
                    await _folBL.AddFollowAsync(fol);
                    return CreatedAtAction("AddFollow", fol);
                }
                catch
                {
                    return StatusCode(400);
                }
            }
            return StatusCode(400);
        }

        // DELETE api/<FollowController>/
        [HttpDelete("{useremail},{followemail}")]
        public async Task<IActionResult> DeleteFollowAsync(string useremail, string followemail)
        {
            try
            {
                var follow = await _folBL.GetSingleUserFollowAsync(useremail, followemail);
                await _folBL.DeleteFollowAsync(follow);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{email}/following")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserFollowsAsync(string email)
        {
            var following = await _folBL.GetUserFollowsAsync(email);
            if (following == null) return NotFound();
            return Ok(following);
        }

        [HttpGet("{email}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetFollowingUserSubscriptionsAsync(string email)
        {
            return Ok(await _folBL.GetFollowingUserSubscriptions(email));
        }
    }
}
