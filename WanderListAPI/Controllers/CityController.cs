using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;
using WanderListAPI.Utility.Poco;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public CityController(WanderListDbContext context, ILogger<City> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<CityController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<CityResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET City all");
            var city = await _context.City
                .Include(cit => cit.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Select(cit => new CityResponse(cit))
                .ToListAsync();

            return Ok(city);
        }

        // GET api/<apiVersion>/<CityController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET City with id {id}");
            var city = await _context.City
                .Include(cit => cit.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Where(cit => cit.CityId == id)
                .Select(cit => new CityResponse(cit))
                .FirstOrDefaultAsync();

            if (city == default(CityResponse))
            {
                return NotFound(new Response()
                {
                    Message = $"No City exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(city);
        }
    }
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/City")]
    [ApiController]
    public class CityContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public CityContentController(WanderListDbContext context, ILogger<City> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}/Content")]
        [ProducesResponseType(typeof(CityContentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Content of city with id {id}");
            var activities = await _context.CityActivity
                .Include(cact => cact.Activity)
                .ThenInclude(act => act.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(item => item.CoverImage)
                .Where(cact => cact.CityId == id)
                .Select(cact => new ItemBriefResponse(cact.Activity))
                .ToListAsync();
            
            var destinations = await _context.CityDestination
                .Include(cdes => cdes.Destination)
                .ThenInclude(des => des.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(item => item.CoverImage)
                .Where(cdes => cdes.CityId == id)
                .Select(cdes => new ItemBriefResponse(cdes.Destination))
                .ToListAsync();

            return Ok(new CityContentResponse(activities, destinations));
        }
    }
}
