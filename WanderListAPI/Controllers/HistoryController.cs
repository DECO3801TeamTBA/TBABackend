using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [Route("api/[controller]")]
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


        // GET api/<HistoryController>/{role}/{id}
        //[Authorize]
        [HttpGet("{role}/{id}")]
        public async Task<IEnumerable<History>> Get(string role, Guid id)
        {
            _logger.LogInformation($"GET History {role} {id}"); 
            if (role == "user")
            {
                var histories = await _context.History
                    .Where(val => val.UserId == id.ToString())
                    .ToListAsync();
                return histories;
            }
            else if (role == "content")
            {
                var histories = await _context.History
                    .Where(hist => hist.ContentId == id)
                    .ToListAsync();
                return histories;
            } else
            {
                //return bad request?
                return null;
            }
        }
    }
}
