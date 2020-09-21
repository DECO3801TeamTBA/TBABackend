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

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public DestinationController(WanderListDbContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<DestinationController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Destination>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Destination all");
            var destination = await _context.Destination
                .Include(des => des.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ToListAsync();

            return Ok(destination);
        }


        // GET api/<apiVersion>/<DestinationController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Destination), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Destination {id}");
            var destination = await _context.Destination
                .Include(des => des.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .Where(des => des.DestinationId == id)
                .FirstOrDefaultAsync();

            if (destination == default(Destination))
            {
                return NotFound(new Response()
                {
                    Message = $"No Destination exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(destination);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Destination")]
    [ApiController]
    public class DestinationResourceMetaController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public DestinationResourceMetaController(WanderListDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Destination/5/Resource
        [HttpGet("{id}/Resource")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ResourceMeta>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Resource for Destination with id {id}");
            var resource = await _context.ContentResourceMeta
                .Include(ires => ires.ResourceMeta)
                .Where(ires => ires.ItemId == id)
                .OrderBy(ires => ires.Number)
                .Select(ires => new {
                    ires.ResourceMeta
                })
                .ToListAsync();

            return Ok(resource);
        }
    }
}
