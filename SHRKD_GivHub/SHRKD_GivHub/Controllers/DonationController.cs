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
    public class DonationController : ControllerBase
    {
        private readonly IDonationBL _donBL;

        public DonationController(IDonationBL donBL)
        {
            _donBL = donBL;
        }
        //ADD api/<LocationController>/
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddDonationAsync([FromBody] Donation donation)
        {
            if (donation.CharityId > 0)
            {
                try
                {
                    await _donBL.AddDonationAsync(donation);
                    return CreatedAtAction("AddDonation", donation);
                }
                catch
                {
                    return StatusCode(400);
                }
            }
            return StatusCode(400);
        }

        // DELETE api/<ChairtyController>/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonationAsync(int id)
        {
            try
            {
                var donation = await _donBL.GetDonationByIdAsync(id);
                await _donBL.DeleteDonationAsync(donation);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        //PUT api/<LocationController>/
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDonationByIdAsync(int id)
        {
            var donation = await _donBL.GetDonationByIdAsync(id);
            if (donation == null) return NotFound();
            return Ok(donation);
        }

        //PUT api/<LocationController>/
        [HttpGet]
        public async Task<IActionResult> GetDonationsAsync()
        {
            return Ok(await _donBL.GetDonationsAsync());
        }

        //PUT api/<LocationController>/
        [HttpGet("/result/{charityid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDonationByCharityAsync(int charityid)
        {
            var donation = await _donBL.GetDonationsByCharityAsync(charityid);
            if (donation == null) return NotFound();
            return Ok(donation);
        }

        //PUT api/<LocationController>/
        [HttpGet("result/{email}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetDonationsByUserAsync(string email)
        {
            if (!email.Equals("") || !email.Equals(null))
            {
                try
                {
                    var donation = await _donBL.GetDonationsByUserAsync(email);
                    if (donation == null) return NotFound();
                    return Ok(donation);
                }
                catch
                {
                    return StatusCode(400);
                }
            }
            return StatusCode(400);
        }

        //PUT api/<LocationController>/
        [HttpPut]
        public async Task<IActionResult> UpdateDonationAsync(Donation donation)
        {
            try
            {
                await _donBL.UpdateDonationAsync(donation);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
