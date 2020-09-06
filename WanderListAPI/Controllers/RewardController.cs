using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
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
    public class RewardController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger<Reward> _logger;

        public RewardController(WanderListDbContext context, ILogger<Reward> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/<apiVersion>/<RewardController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET all Reward");
            //If none exists, just return empty list.
            return Ok(await _context.Reward.ToListAsync());
        }

        // GET api/<apiVersion>/<RewardController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Reward with {id}");
            var reward = await _context.Reward.FindAsync(id);
            if (reward == default(Reward))
            {
                return NotFound(new Response()
                {
                    Message = $"No reward exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(reward);
        }

        // POST api/<apiVersion>/<RewardController>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] Reward reward)
        {
            _logger.LogInformation($"POST Reward");
            try
            {
                _context.Reward.Add(reward);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return CreatedAtAction(nameof(Post), new { id = reward.RewardId }, reward);
        }

        // PUT api/<apiVersion>/<RewardController>/5
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Reward reward)
        {
            _logger.LogInformation($"PUT Reward with {id}");
            try
            {
                _context.Reward.Update(reward);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return NoContent();
        }

        // PUT api/<apiVersion>/<RewardController>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] IEnumerable<Reward> rewards)
        {
            _logger.LogInformation($"PUT Rewards");
            if (rewards.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "No rewards to update",
                    Status = "400"
                });
            }
            try
            {
                _context.Reward.UpdateRange(rewards);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return NoContent();
        }

        // DELETE api/<apiVersion>/<RewardController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"DELETE Reward with {id}");

            var reward = await _context.Reward.FindAsync(id);
            if (reward == default(Reward))
            {
                return NotFound(new Response()
                {
                    Message = $"No reward exists with id {id}",
                    Status = "404"
                });
            }
            try
            {
                _context.Reward.Remove(reward);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return NoContent();
        }

        // DELETE api/<apiVersion>/<RewardController>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete()
        {
            _logger.LogInformation($"DELETE all Rewards");
            try
            {
                //Fastest way to delete them all
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Reward");
            }
            catch (Exception ex)
            {
                return BadRequest(new Response()
                {
                    Message = ex.Message,
                    Status = "400"
                });
            }
            return NoContent();
        }
    }
}