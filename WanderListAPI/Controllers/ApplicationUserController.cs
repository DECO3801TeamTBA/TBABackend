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

        public ApplicationUserController(WanderListDbContext context, ILogger<ApplicationUser> logger)
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
}
