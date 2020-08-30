using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

namespace WanderListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public RestaurantController(WanderListDbContext context, ILogger<History> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<ContentController>/all
        [HttpGet("all")]
        public async Task<IEnumerable<Content>> Get(String type)
        {
            _logger.LogInformation($"GET Content {type}");
            var content = await _context.Restaurant
                .ToListAsync();
            return content;
        }


        // GET api/<ContentController>/5
        [HttpGet("{restaurantId}")]
        public async Task<Content> Get(Guid restaurantId)
        {
            _logger.LogInformation($"GET Content {restaurantId}");
            var content = await _context.Restaurant
                .Where(val => val.ContentId == restaurantId)
                .FirstOrDefaultAsync();
            return content;
        }
    }
}
