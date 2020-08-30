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
    public class ApplicationUserController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ApplicationUserController(WanderListDbContext context, ILogger<History> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<ApplicationUserController>/all
        [HttpGet("all")]
        public async Task<IEnumerable<ApplicationUser>> Get()
        {
            _logger.LogInformation($"GET Users all");
            var users = await _context.ApplicationUser
                .ToListAsync();
            return users;
        }

        // GET api/<ApplicationUserController>/5
        [HttpGet("{userid}")]
        public async Task<ApplicationUser> Get(int userId)
        {
            _logger.LogInformation($"GET Users {userId}");
            var user = await _context.ApplicationUser
                .Where(val => val.Id == userId.ToString())
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
