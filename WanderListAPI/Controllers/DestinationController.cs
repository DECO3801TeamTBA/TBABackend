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
    public class DestinationController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public DestinationController(WanderListDbContext context, ILogger<Destination> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<DestinationController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<DestinationResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Destination all");
            var destination = await _context.Destination
                .Include(des => des.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Select(ite => new DestinationResponse(ite))
                .ToListAsync();

            return Ok(destination);
        }


        // GET api/<apiVersion>/<DestinationController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(DestinationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Destination {id}");
            var destination = await _context.Destination
                .Include(des => des.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Where(des => des.DestinationId == id)
                .Select(des => new DestinationResponse(des))
                .FirstOrDefaultAsync();

            if (destination == default(DestinationResponse))
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
}
