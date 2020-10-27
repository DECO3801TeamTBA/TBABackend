using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;
using WanderListAPI.Utility.Poco;

namespace WanderListAPI.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MapViewController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public MapViewController(WanderListDbContext context, ILogger<MapResponse> logger)
        {
            _logger = logger;
            _context = context;
        }
        
        /**
         * The following code isn't very restful as it will calculate capacity for each content being returned
         * capacity will be a value from 0-5 >> Math.Round number of visits in the past... 2 weeks to now / capacity?
         * */
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<MapResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Map data");
            var destinations = await _context.Destination
                .Include(des => des.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ToListAsync();
            var activities = await _context.Activity
                .Include(act => act.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ToListAsync();

            //calculate capacities => make a dictonary (id -> capacity)

            var histories = await _context.History.AsNoTracking().ToListAsync();

            var histDictionary = histories
                .Where(h => DateTime.Now - h.Date < TimeSpan.FromDays(14))
                .GroupBy(h => h.ContentId)
                .ToDictionary(h => h.Key, g => g.Count());

            static int getCapacity(int visits, int totalCapacity) => Math.Min((int)Math.Round(((double)visits / (double)totalCapacity) * (double)totalCapacity), 5);

            var mapResponses = destinations.Select(des => new MapResponse()
            {
                Id = des.DestinationId,
                Name = des.Content.Item.Name,
                AverageRating = (des.Content.EconomicRating + des.Content.EnvironmentalRating + des.Content.EnvironmentalRating) / 3,
                Capacity = histDictionary.ContainsKey(des.DestinationId) ? getCapacity(histDictionary[des.DestinationId], des.Content.Capacity) : 0,
                Description = des.Content.Item.Description,
                Latitude = des.Content.Item.Lattitude,
                Longitude = des.Content.Item.Longitude,
                Type = "destination",    
                CoverImage = des.Content.Item.CoverImage
            }).ToList();
            mapResponses.AddRange(activities.Select(act => new MapResponse()
            {
                Id = act.ActivityId,
                Name = act.Content.Item.Name,
                AverageRating = (act.Content.EconomicRating + act.Content.EnvironmentalRating + act.Content.EnvironmentalRating) / 3,
                Capacity = histDictionary.ContainsKey(act.ActivityId) ? getCapacity(histDictionary[act.ActivityId], act.Content.Capacity) : 0,
                Description = act.Content.Item.Description,
                Latitude = act.Content.Item.Lattitude,
                Longitude = act.Content.Item.Longitude,
                Type = "activity",
                CoverImage = des.Content.Item.CoverImage
            }).ToList());

            return Ok(mapResponses);
        }
    }
}
