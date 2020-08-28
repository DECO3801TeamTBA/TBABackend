using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public HistoryController(WanderListDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        // GET api/<HistoryController>/5
        [HttpGet("/{role}/{id}")]
        public async Task<IEnumerable<History>> Get(string role, Guid id)

        {
            if (role == "user")
            {
                var histories = await _context.History
                    .Where(hist => hist.WanderUserId == id)
                    .ToListAsync();
                return histories;
            }
            else
            {
                var histories = await _context.History
                    .Where(hist => hist.ContentId == id)
                    .ToListAsync();
                return histories;
            }
        }

        // POST api/<HistoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HistoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HistoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
