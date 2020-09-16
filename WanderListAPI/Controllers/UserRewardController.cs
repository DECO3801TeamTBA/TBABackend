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

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserRewardController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;
        public UserRewardController(WanderListDbContext context, ILogger<UserReward> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ShortlistController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserReward>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET rewards for user {id}");
            var userReward = await _context.UserReward
                    .Where(val => val.UserId == id.ToString())
                    .ToListAsync();
            return Ok(userReward);
        }
    }
}
