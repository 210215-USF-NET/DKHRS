using GivHubBL;
using GivHubModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SHRKD_GivHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationBL _locBL;


        public LocationController(ILocationBL locBL)
        {
            _locBL = locBL;
        }

        // POST api/<LocationController>
        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> AddLocationAsync([FromBody] Location location)
        {
            try
            {
                await _locBL.AddLocationAsync(location);
                return CreatedAtAction("AddLocation", location);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        // DELETE api/<LocationController>/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationAsync(int id)
        {
            try
            {
                var location = await _locBL.GetLocationByIdAsync(id);
                await _locBL.DeleteLocationAsync(location);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        //GET api/<LocationController>/
        [HttpGet("{city},{state}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLocationByCityStateAsync(string city, string state)
        {
            var location = await _locBL.GetLocationByCityStateAsync(city, state);
            if (location == null) return NotFound();
            return Ok(location);
        }

        //GET api/<LocationController>/
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLocationByIdAsync(int id)
        {
            var location = await _locBL.GetLocationByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        //GET api/<LocationController>/
        [HttpGet]
        public async Task<IActionResult> GetLocationsAsync()
        {
            return Ok(await _locBL.GetLocationsAsync());
        }

        //PUT api/<LocationController>/
        [HttpPut]
        public async Task<IActionResult> UpdateLocationAsync(Location location)
        {
            try
            {
                await _locBL.UpdateLocationAsync(location);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
