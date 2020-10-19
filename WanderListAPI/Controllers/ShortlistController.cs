using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        [ProducesResponseType(typeof(Shortlist), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Shortlists with id {id}");
            var shortlist = await _context.Shortlist
                    .Where(sho => sho.ShortlistId == id)
                    .FirstOrDefaultAsync();

            var contents = await _context.ShortlistContent
                .Include(sl => sl.Content)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.CoverImage)
                .Where(sl => sl.ShortlistId == id)
                .ToListAsync();

            if (shortlist == default(Shortlist))
            {
                return NotFound(new Response()
                {
                    Message = $"No Shortlist exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(new ShortlistResponse()
            {
                ShortlistId = shortlist.ShortlistId,
                ListName = shortlist.ListName,
                CoverImage = contents.Count > 0 ? new ResourceResponse(contents[0].Content.Item.CoverImage) : null 
            });
        }

        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Shortlist), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Shortlist shortlist)
        {
            _logger.LogInformation($"POST Shortlist");
            try
            {
                _context.Shortlist.Add(shortlist);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return CreatedAtAction(nameof(Post), new { id = shortlist.ShortlistId }, shortlist);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Shortlist")]
    [ApiController]
    public class ShortlistContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ShortlistContentController(WanderListDbContext context, ILogger<ShortlistContent> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Shortlist/5/Content
        [HttpGet("{id}/Content")]
        [Authorize]
        [ProducesResponseType(typeof(List<ItemBriefResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET content for shortlist with id {id}");
            var shortlistContent = await _context.ShortlistContent
                .Include(scon => scon.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .Where(scon => scon.ShortlistId == id)
                .OrderBy(scon => scon.Number)
                .Select(scon => new ItemBriefResponse(scon.Content))
                .ToListAsync();

            return Ok(shortlistContent);
        }

        [HttpPost("{id}/Content")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<Shortlist>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Guid id, [FromBody] List<ShortlistContent> shortlistContents)
        {
            _logger.LogInformation($"POST ShortlistContent with Shortlist id: {id}");
            try
            {
                _context.ShortlistContent.AddRange(shortlistContents);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return CreatedAtAction(nameof(Post), new { id = id.ToString() }, shortlistContents);
        }

    }
}
