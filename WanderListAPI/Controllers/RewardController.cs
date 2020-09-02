using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/<RewardController>
        [HttpGet]
        public async Task<IEnumerable<Reward>> Get()
        {
            _logger.LogInformation($"GET all Reward");
            //If none exists, just return empty list.
            return await _context.Reward.ToListAsync();
        }

        // GET api/<RewardController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Reward with {id}");
            var result = await _context.Reward.FindAsync(id);
            if (result == default(Reward))
            {
                return BadRequest(new Response()
                {
                    Message = "No reward exists with id " + id.ToString(),
                    Status = "400"
                });
            }

            return Ok(result);
        }

        // POST api/<RewardController>
        [HttpPost]
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
            return NoContent();
        }

        // PUT api/<RewardController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Reward reward)
        {
            _logger.LogInformation($"PUT Reward with {id}");
            try
            {
                var result = await _context.Reward.FindAsync(id);
                if (result == default(Reward))
                {
                    return BadRequest(new Response()
                    {
                        Message = "Could not find reward with id " + id.ToString(),
                        Status = "400"
                    });
                }
                result = reward;
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] IEnumerable<Reward> rewards)
        {
            _logger.LogInformation($"PUT Rewards");
            if (rewards.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "You must include rewards to update",
                    Status = "400"
                });
            }
            try
            {
                _context.Reward.AddRange(rewards);
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

        // DELETE api/<RewardController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"DELETE Reward with {id}");
            try
            {
                _context.Reward.Remove(new Reward() { RewardId = id });
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


        [HttpDelete]
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