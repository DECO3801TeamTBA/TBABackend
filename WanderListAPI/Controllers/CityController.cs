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

        public CityController(WanderListDbContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<CityController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<City>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET City all");
            var city = await _context.City
                .Include(cit => cit.Item)
                .ThenInclude(i => i.CoverImage)
                .ToListAsync();

            return Ok(city);
        }

        // GET api/<apiVersion>/<CityController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET City with id {id}");
            var city = await _context.City
                .Include(cit => cit.Item)
                .ThenInclude(i => i.CoverImage)
                .Where(cit => cit.CityId == id)
                .FirstOrDefaultAsync();

            if (city == default(City))
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
}
