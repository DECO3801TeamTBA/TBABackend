using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShortlistContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;
        public ShortlistContentController(WanderListDbContext context, ILogger<ShortlistContent> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<UserRewardController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET content for shortlist {id}");
            var shortlistContent = await _context.ShortlistContent
                    .Where(val => val.ShortlistId == id)
                    .ToListAsync();
            return Ok(shortlistContent);
        }
    }
}
