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
    public class SearchHistoryController : Controller
    {
        private readonly ISearchHistoryBL _shBL;


        public SearchHistoryController(ISearchHistoryBL shBL)
        {
            _shBL = shBL;
        }

        // POST api/<SearchHistoryController>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddSearchHistoryAsync([FromBody] SearchHistory sh)
        {
            try
            {
                await _shBL.AddSearchHistoryAsync(sh);
                return CreatedAtAction("AddSearchHistory", sh);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        // DELETE api/<SearchHistoryController>/
        [HttpDelete("{email},{phrase}")]
        public async Task<IActionResult> DeleteSearchHistoryAsync(string email, string phrase)
        {
            try
            {
                var sh = await _shBL.GetUserSingleSearchHistoryAsync(email, phrase);
                await _shBL.DeleteSearchHistoryAsync(sh);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        //GET api/<LocationController>/
        [HttpGet]
        public async Task<IActionResult> GetSearchHistoriesAsync()
        {
            return Ok(await _shBL.GetSearchHistoriesAsync());
        }

        //GET api/<LocationController>/
        [HttpGet("{email}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSearchHistoriesByUserAsync(string email)
        {
            var sh = await _shBL.GetSearchHistoriesByUserAsync(email);
            if (sh == null) return NotFound();
            return Ok(sh);
        }

        //PUT api/<LocationController>/
        [HttpPut]
        public async Task<IActionResult> UpdateSearchHistoryAsync(SearchHistory sh)
        {
            try
            {
                await _shBL.UpdateSearchHistoryAsync(sh);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
