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
using WanderListAPI.Utility.Poco;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public UserController(WanderListDbContext context, ILogger<AppUser> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ApplicationUserController>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserResponseBrief>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Users all");
            var users = await _context.ApplicationUser
                .Select(use => new UserResponseBrief(use))
                .ToListAsync();
            return Ok(users);
        }

        // GET api/<apiVersion>/<ApplicationUserController>/5
        [HttpGet("{userid}")]
        [ProducesResponseType(typeof(UserResponseBrief), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserResponseBrief), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"GET Users {id}");
            var user = await _context.ApplicationUser
                .Where(use => use.Id == id.ToString())
                .Select(use => new UserResponseBrief(use))
                .FirstOrDefaultAsync();

            if (user == default(UserResponseBrief))
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
    [Route("api/v{version:apiVersion}/User")]
    [ApiController]
    public class UserRewardController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public UserRewardController(WanderListDbContext context, ILogger<AppUser> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/User/5/Reward
        [HttpGet("{id}/Reward")]
        [Authorize]
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
    [Route("api/v{version:apiVersion}/User")]
    [ApiController]
    public class UserHistoryController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger<History> _logger;

        public UserHistoryController(WanderListDbContext context, ILogger<History> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/User/5/History
        [HttpGet("{id}/History")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET History for User with id {id}");
            var history = await _context.History
                .Where(hist => hist.UserId == id.ToString())
                .Select(hist => new UserHistoryResponse(hist))
                .ToListAsync();

            return Ok(history);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/User")]
    [ApiController]
    public class UserShortlistController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger<Shortlist> _logger;

        public UserShortlistController(WanderListDbContext context, ILogger<Shortlist> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/User/5/Shortlist
        [HttpGet("{id}/Shortlist")]
        [Authorize]
        [ProducesResponseType(typeof(List<ShortlistResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Shortlist for User with id {id}");
            var shortlists = await _context.Shortlist
                .Where(shor => shor.UserId == id.ToString())
                .ToListAsync();
            var shortlistIds = shortlists.Select(sl => sl.ShortlistId).ToList();
            var contents = await _context.ShortlistContent
                .Include(sl => sl.Content)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.CoverImage)
                .Where(slc => shortlistIds.Contains(slc.ShortlistId))
                .ToListAsync();

            var contentLookup = contents.ToLookup(slc => slc.ShortlistId);

            var result = new List<ShortlistResponse>();
            shortlists.ForEach(s => {
                var content = contentLookup[s.ShortlistId];
                result.Add(new ShortlistResponse()
                {
                    ShortlistId = s.ShortlistId,
                    ListName = s.ListName,
                    CoverImage = content.Any() ? new ResourceResponse(content.First().Content.Item.CoverImage) : null
                });
            });

            


            return Ok(result);
        }
    }
}
