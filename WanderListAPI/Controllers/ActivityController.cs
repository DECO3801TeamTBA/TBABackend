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
                .Select(ite => new ItemBriefResponse(ite))
                .ToListAsync();

            return Ok(activity);
        }


        // GET api/<apiVersion>/<ActivityController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Activity), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Activity with id {id}");
            var activity = await _context.Activity
                .Include(act => act.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .Where(act => act.ActivityId == id)
                .FirstOrDefaultAsync();

            if (activity == default(Activity))
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

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Activity")]
    [ApiController]
    public class ActivityResourceMetaController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ActivityResourceMetaController(WanderListDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Activity/5/Resource
        [HttpGet("{id}/Resource")]
        [Authorize]
        [ProducesResponseType(typeof(List<ResourceMeta>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Resource for Activity with id {id}");
            var resource = await _context.ContentResourceMeta
                .Include(ires => ires.ResourceMeta)
                .Where(ires => ires.ContentId == id)
                .OrderBy(ires => ires.Number)
                .Select(ires => new {
                    ires.ResourceMeta
                })
                .ToListAsync();

            return Ok(resource);
        }
    }
}
