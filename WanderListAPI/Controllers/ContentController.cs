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
    public class ContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ContentController(WanderListDbContext context, ILogger<Content> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<ContentController>/all
        [HttpGet("all")]
        public async Task<IEnumerable<Content>> Get()
        {
            _logger.LogInformation($"GET Content all");
            var content = await _context.Content
                .ToListAsync();
            return content;
        }


        // GET api/<ContentController>/5
        [HttpGet("{contentId}")]
        public async Task<Content> Get(Guid contentId)
        {
            _logger.LogInformation($"GET Content {contentId}");
            var content = await _context.Content
                .Where(val => val.ContentId == contentId)
                .FirstOrDefaultAsync();
            return content;
        }
    }
}
