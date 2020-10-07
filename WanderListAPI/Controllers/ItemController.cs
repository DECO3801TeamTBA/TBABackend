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
    public class ItemController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ItemController(WanderListDbContext context, ILogger<Item> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ItemController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<ItemBriefResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Item all");
            var item = await _context.Item
                .Include(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Select(ite => new ItemBriefResponse(ite))
                .ToListAsync();

            return Ok(item);
        }

        // GET api/<apiVersion>/<ItemController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Item with id {id}");
            var item = await _context.Item
                .Include(ite => ite.CoverImage)
                .ThenInclude(resm => resm.Resource)
                .Where(ite => ite.ItemId == id)
                .Select(ite => new ItemResponse(ite))
                .FirstOrDefaultAsync();

            if (item == default(ItemResponse))
            {
                return NotFound(new Response()
                {
                    Message = $"No Item exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(item);
        }
    }
}
