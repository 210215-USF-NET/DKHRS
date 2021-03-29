using GivHubBL;
using GivHubModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SHRKD_GivHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharityController : Controller
    {

        private readonly ICharityBL _charBL;

        public CharityController(ICharityBL charBL)
        {
            _charBL = charBL;
        }

        //POST
        [HttpPost]
        [Consumes("application/json")]
        [AcceptVerbs("POST")]
        public async Task<IActionResult> AddCharityAsync([FromBody] object thisJSON)
        {
            try
            {
                var charities = JsonConvert.DeserializeObject<Charity[]>(thisJSON.ToString());
                foreach (Charity ch in charities)
                {
                    Debug.WriteLine(ch.Name);
                    var findCharity = await _charBL.GetCharityByNameAsync(ch.Name);
                    if (findCharity == null) {
                        await _charBL.AddCharityAsync(ch);
                    } 
                }
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        // DELETE api/<ChairtyController>/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharityAsync(int id)
        {
            try
            {
                var charity = await _charBL.GetCharityByIdAsync(id);
                await _charBL.DeleteCharityAsync(charity);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        //GET api/<LocationController>/
        [HttpGet]
        public async Task<IActionResult> GetCharitiesAsync()
        {
            return Ok(await _charBL.GetCharitiesAsync());
        }

        [HttpGet("category")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCharitiesByCategoryAsync(string category)
        {
            var charities = await _charBL.GetCharitiesByCategoryAsync(category);
            if (charities == null) return NotFound();
            return Ok(charities);
        }

        [HttpGet("id")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCharityByIdAsync(int id)
        {
            var charity = await _charBL.GetCharityByIdAsync(id);
            if (charity == null) return NotFound();
            return Ok(charity);
        }

        [HttpGet("eid")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCharityByEidAsync(string eid)
        {
            var charity = await _charBL.GetCharityByEidAsync(eid);
            if (charity == null) return NotFound();
            return Ok(charity);
        }

        [HttpGet("name")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCharityByNameAsync(string name)
        {
            var charity = await _charBL.GetCharityByNameAsync(name);
            if (charity == null) return NotFound();
            return Ok(charity);
        }

        [HttpGet("website")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCharityByWebsiteAsync(string website)
        {
            var charity = await _charBL.GetCharityByWebsiteAsync(website);
            if (charity == null) return NotFound();
            return Ok(charity);
        }

        [HttpGet("popularcharity")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPopularCharitiesAsync()
        {
            return Ok(await _charBL.GetPopularCharitiesAsync());
        }


        //PUT api/<LocationController>/
        [HttpPut]
        public async Task<IActionResult> UpdateCharityAsync(Charity charity)
        {
            try
            {
                await _charBL.UpdateCharityAsync(charity);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
