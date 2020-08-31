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
    public class DestinationController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public DestinationController(WanderListDbContext context, ILogger<History> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<ContentController>/all
        [HttpGet("all")]
        public async Task<IEnumerable<Destination>> Get(String type)
        {
            _logger.LogInformation($"GET Content {type}");
            var content = await _context.Destination
                .Include(dest => dest.Content)
                .ToListAsync();
            return content;
        }


        // GET api/<ContentController>/5
        [HttpGet("{destinationId}")]
        public async Task<Destination> Get(Guid destinationId)
        {
            _logger.LogInformation($"GET Content {destinationId}");
            var content = await _context.Destination
                .Include(dest => dest.Content)
                .Where(val => val.Content.ContentId == destinationId)
                .FirstOrDefaultAsync();
            return content;
        }
    }
}
