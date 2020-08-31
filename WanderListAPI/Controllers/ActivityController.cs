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
    public class ActivityController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ActivityController(WanderListDbContext context, ILogger<Activity> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<ActivityController>/all
        [HttpGet("all")]
        public async Task<IEnumerable<Activity>> Get()
        {
            _logger.LogInformation($"GET Activity all");
            var content = await _context.Activity
                .Include(act => act.Content)
                .ToListAsync();
            return content;
        }


        // GET api/<ActivityController>/5
        [HttpGet("{activityId}")]
        public async Task<Activity> Get(Guid activityId)
        {
            _logger.LogInformation($"GET Activity {activityId}");
            var content = await _context.Activity
                .Include(act => act.Content)
                .Where(val => val.Content.ContentId == activityId)
                .FirstOrDefaultAsync();
            return content;
        }
    }
}
