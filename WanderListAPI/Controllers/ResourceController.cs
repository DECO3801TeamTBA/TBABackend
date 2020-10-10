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
using WanderListAPI.Utility.Poco;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ResourceController(WanderListDbContext context, ILogger<ResourceMeta> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/<apiVersion>/<ResourceMetaController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<FileResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET ResourceMeta all");
            var resource = await _context.ResourceMeta
                .Include(res => res.Resource)
                .Select(res => ResourceResponse.GetFile(res))
                .ToListAsync();

            return Ok(resource);
        }

        // GET api/<apiVersion>/<ResourceController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Resource with {id}");
            var resourceMeta = await _context.ResourceMeta
                .Include(res => res.Resource)
                .FirstOrDefaultAsync(res => res.ResourceMetaId == id);

            if (resourceMeta == default(ResourceMeta))
            {
                return NotFound(new Response()
                {
                    Message = $"No Resource exists with id {id}",
                    Status = "404"
                });
            }

            if (resourceMeta.OnDisk)
            {
                //Assuming virtual, havent decided yet, probably virtual....
                return File(resourceMeta.Resource.FilePath, resourceMeta.MimeType);
            }
            return File(resourceMeta.Resource.Data, resourceMeta.MimeType);
        }
    }
}
