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
    public class ShortlistController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;
        public ShortlistController(WanderListDbContext context, ILogger<Shortlist> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ShortlistController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Shortlist>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Shortlist all");

            return Ok(await _context.Shortlist.ToListAsync());
        }

        // GET: api/<apiVersion>/<ShortlistController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Shortlist>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Shortlists with id {id}");
            var shortlist = await _context.Shortlist
                    .Where(sho => sho.ShortlistId == id)
                    .FirstOrDefaultAsync();

            if (shortlist == default(Shortlist))
            {
                return NotFound(new Response()
                {
                    Message = $"No Shortlist exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(shortlist);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Shortlist")]
    [ApiController]
    public class ShortlistContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ShortlistContentController(WanderListDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Shortlist/5/Content
        [HttpGet("{id}/Content")]
        [Authorize]
        [ProducesResponseType(typeof(List<Content>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET content for shortlist with id {id}");
            var shortlistContent = await _context.ShortlistContent
                .Include(scon => scon.Content)
                .Where(scon => scon.ShortlistId == id)
                .OrderBy(scon => scon.Number)
                .Select(scon => new ItemBriefResponse(scon.Content))
                .ToListAsync();

            return Ok(shortlistContent);
        }
    }
}
