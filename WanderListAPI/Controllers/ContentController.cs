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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ContentController(WanderListDbContext context, ILogger<Content> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ContentController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Content all");
            var content = await _context.Content
                .ToListAsync();
            return Ok(content);
        }


        // GET api/<ContentController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Content {id}");
            var content = await _context.Content
                .Where(val => val.ContentId == id)
                .FirstOrDefaultAsync();

            if (content == default(Content))
            {
                return NotFound(new Response()
                {
                    Message = $"No content exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(content);
        }
    }
}
