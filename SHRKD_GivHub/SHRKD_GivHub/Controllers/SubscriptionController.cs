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
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionBL _subBL;

        public SubscriptionController(ISubscriptionBL subBL)
        {
            _subBL = subBL;
        }

        //POST
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddSubscriptionAsync([FromBody] Subscription subscription)
        {
            try
            {
                var findSub = await _subBL.GetSingleUserSubscription(subscription.Email, subscription.CharityId);
                if (findSub != null) return NotFound();
                await _subBL.AddSubscriptionAsync(subscription);
                return CreatedAtAction("AddSubscription", subscription);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        // DELETE api/<SubscriptionController>/
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSubscriptionAsync(string email, int charityval)
        {
            try
            {
                var subscription = await _subBL.GetSingleUserSubscription(email, charityval);
                await _subBL.DeleteSubscriptionAsync(subscription);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        //GET api/<SubscriptionController>/
        [HttpGet]
        public async Task<IActionResult> GetSubscriptionsAsync()
        {
            return Ok(await _subBL.GetSubscriptionsAsync());
        }

        [HttpGet("email")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSubscriptionsByUserAsync(string email)
        {
            var donations = await _subBL.GetSubscriptionsByUserAsync(email);
            if (donations == null) return NotFound();
            return Ok(donations);
        }

        //PUT api/<SubscriptionController>/
        [HttpPut]
        public async Task<IActionResult> UpdateSubscriptionAsync(Subscription subscription)
        {
            try
            {
                await _subBL.UpdateSubscriptionAsync(subscription);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
