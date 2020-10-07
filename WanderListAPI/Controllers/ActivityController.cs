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
        [ProducesResponseType(typeof(List<ItemBriefResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Activity all");
            var activity = await _context.Activity
                .Include(act => act.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Select(ite => new ItemBriefResponse(ite))
                .ToListAsync();

            return Ok(activity);
        }


        // GET api/<apiVersion>/<ActivityController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActivityResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Activity with id {id}");
            var activity = await _context.Activity
                .Include(act => act.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Where(act => act.ActivityId == id)
                .Select(act => new ActivityResponse(act))
                .FirstOrDefaultAsync();

            if (activity == default(ActivityResponse))
            {
                return NotFound(new Response()
                {
                    Message = $"No Activity exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(activity);
        }
    }
}
