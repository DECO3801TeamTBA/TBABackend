using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        // GET: api/<apiVersion>/<ActivityController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Activity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Activity all");
            var activity = await _context.Activity
                .Include(act => act.Content)
                .ToListAsync();
            return Ok(activity);
        }


        // GET api/<apiVersion>/<ActivityController>/5
        [HttpGet("{activityId}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Activity), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Activity {id}");
            var activity = await _context.Activity
                .Include(act => act.Content)
                .Where(val => val.Content.ContentId == id)
                .FirstOrDefaultAsync();

            if (activity == default(Activity))
            {
                return NotFound(new Response()
                {
                    Message = $"No activity exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(activity);
        }
    }
}
