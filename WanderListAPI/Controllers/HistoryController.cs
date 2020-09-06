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
    public class HistoryController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;
        public HistoryController(WanderListDbContext context, ILogger<History> logger)
        {
            _logger = logger;
            _context = context;
        }


        // GET api/<apiVersion>/<HistoryController>/user/5
        //[Authorize]
        [HttpGet("{role}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string role, Guid id)
        {
            _logger.LogInformation($"GET History {role} {id}"); 
            if (role == "user")
            {
                var histories = await _context.History
                    .Where(val => val.UserId == id.ToString())
                    .ToListAsync();
                return Ok(histories);
            }
            else if (role == "content")
            {
                var histories = await _context.History
                    .Where(hist => hist.ContentId == id)
                    .ToListAsync();
                return Ok(histories);
            } else
            {
                return BadRequest(new Response()
                {
                    Message = $"Role {role} does not exist",
                    Status = "404"
                });
            }
        }


        // GET api/<apiVersion>/<HistoryController>/user/5
        //[Authorize]
        [HttpGet("{role}/{id}")]
        public async Task<IActionResult> Get(string role, Guid id, [FromBody] DateTime start, [FromBody] DateTime end)
        {
            _logger.LogInformation($"GET History {role} {id}");
            if (role == "user")
            {
                var histories = await _context.History
                    .Where(hist => hist.UserId == id.ToString() && hist.Date >= start && hist.Date <= end)
                    .ToListAsync();
                return Ok(histories);
            }
            else if (role == "content")
            {
                var histories = await _context.History
                    .Where(hist => hist.ContentId == id && hist.Date >= start && hist.Date <= end)
                    .ToListAsync();
                return Ok(histories);
            }
            else
            {
                return BadRequest(new Response()
                {
                    Message = $"Role {role} does not exist",
                    Status = "404"
                });
            }
        }
    }
}
