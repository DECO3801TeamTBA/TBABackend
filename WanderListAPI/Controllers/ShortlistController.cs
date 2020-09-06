using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

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
        [HttpGet("{idType}{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id, string idType)
        {
            if (idType == "")
            {
                _logger.LogInformation($"GET shortlist {id}");
                var shortlist = await _context.Shortlist
                        .Where(val => val.ShortlistId == id)
                        .ToListAsync();
                return Ok(shortlist);
            } 
            else if (idType == "user/")
            {
                _logger.LogInformation($"GET shortlists for user {id}");
                var shortlist = await _context.Shortlist
                        .Where(val => val.UserId == id.ToString())
                        .ToListAsync();
                return Ok(shortlist);
            } 
            else
            {
                return BadRequest();
            }
            
        }
    }
}
