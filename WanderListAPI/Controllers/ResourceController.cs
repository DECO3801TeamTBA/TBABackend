using System;
using System.Collections.Generic;
using System.IO;
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
    public class ResourceController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger<Resource> _logger;

        public ResourceController(WanderListDbContext context, ILogger<Resource> logger)
        {
            _context = context;
            _logger = logger;
        }

        // we don't actually want this!
        // GET: api/<apiVersion>/<ResourceController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Resource>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET all Resource");
            //If none exists, just return empty list.
            return Ok(await _context.Resource.ToListAsync());
        }

        // GET api/<apiVersion>/<ResourceController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Resource), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Resource with {id}");
            var resourceMeta = await _context.ResourceMeta.FindAsync(id);
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
                //get file from disk, convert to byte array
                // send as file result
                var filePath = Path.Combine(resourceMeta.Resource.FilePath, resourceMeta.Extension); //?
                return PhysicalFile(filePath, resourceMeta.MimeType);
            }
            return new FileContentResult(resourceMeta.Resource.Data, resourceMeta.MimeType);
        }
    }
}