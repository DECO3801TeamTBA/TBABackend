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
    public class ApplicationUserController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ApplicationUserController(WanderListDbContext context, ILogger logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ApplicationUserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Users all");
            var users = await _context.ApplicationUser
                .Select(val => new { 
                    val.UserName, 
                    val.FirstName, 
                    val.LastName, 
                    val.Email, 
                    val.PhoneNumber, 
                    val.Points})
                .ToListAsync();
            return Ok(users);
        }

        // GET api/<apiVersion>/<ApplicationUserController>/5
        [HttpGet("{userid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"GET Users {id}");
            var user = await _context.ApplicationUser
                .Where(val => val.Id == id.ToString())
                .Select(val => new {
                    val.UserName,
                    val.FirstName,
                    val.LastName,
                    val.Email,
                    val.PhoneNumber,
                    val.Points
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new Response()
                {
                    Message = $"No user exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(user);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ApplicationUser")]
    [ApiController]
    public class UserRewardController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public UserRewardController(WanderListDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/ApplicationUser/5/Reward
        [HttpGet("{id}/Reward")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Reward>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Reward for User with id {id}");
            var reward = await _context.UserReward
                .Include(urew => urew.Reward)
                .Where(urew => urew.UserId == id.ToString())
                .Select(urew => urew.Reward)
                .ToListAsync();

            return Ok(reward);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ApplicationUser")]
    [ApiController]
    public class ApplicationUserHistoryController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger<History> _logger;

        public ApplicationUserHistoryController(WanderListDbContext context, ILogger<History> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/ApplicationUser/5/History
        [HttpGet("{id}/History")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<History>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET History for User with id {id}");
            var history = await _context.History
                .Where(hist => hist.UserId == id.ToString())
                .Select(hist => new {
                    hist.ContentId,
                    hist.Date
                })
                .ToListAsync();

            return Ok(history);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ApplicationUser")]
    [ApiController]
    public class ApplicationUserShortlistController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger<Shortlist> _logger;

        public ApplicationUserShortlistController(WanderListDbContext context, ILogger<Shortlist> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/ApplicationUser/5/Shortlist
        [HttpGet("{id}/Shortlist")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<Shortlist>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Shortlist for User with id {id}");
            var shortlist = await _context.UserShortlist
                .Include(usho => usho.Shortlist)
                .Where(usho => usho.UserId == id.ToString())
                .Select(usho => usho.Shortlist)
                .ToListAsync();

            return Ok(shortlist);
        }
    }
}
