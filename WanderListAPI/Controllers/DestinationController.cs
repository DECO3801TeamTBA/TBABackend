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
        [ProducesResponseType(typeof(List<Destination>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Destination all");
            var destination = await _context.Destination
                .Include(dest => dest.Content)
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
                .Include(dest => dest.Content)
                .Where(val => val.Content.ContentId == id)
                .FirstOrDefaultAsync();

            if (destination == default(Destination))
            {
                return NotFound(new Response()
                {
                    Message = $"No destination exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(destination);
        }
    }
}
