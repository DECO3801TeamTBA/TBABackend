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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceMetaController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ResourceMetaController(WanderListDbContext context, ILogger logger)
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
                .ToListAsync();

            var result = new List<FileResult>();
            foreach (var element in resource)
            {
                if (element.OnDisk)
                {
                    //Assuming virtual, havent decided yet, probably virtual....
                    result.Add(File(element.Resource.FilePath, element.MimeType));
                } else
                {
                    result.Add(File(element.Resource.Data, element.MimeType));
                }
            }

            return Ok(result);
        }

        // GET api/<apiVersion>/<ResourceMetaController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET ResourceMeta with {id}");
            var resourceMeta = await _context.ResourceMeta
                .Include(res => res.Resource)
                .FirstOrDefaultAsync(res => res.ResourceMetaId == id);

            if (resourceMeta == default(ResourceMeta))
            {
                return NotFound(new Response()
                {
                    Message = $"No ResourceMeta exists with id {id}",
                    Status = "404"
                });
            }

            if (resourceMeta.OnDisk)
            {
                //Assuming virtual, havent decided yet, probably virtual....
                return Ok(File(resourceMeta.Resource.FilePath, resourceMeta.MimeType));
            }
            return Ok(File(resourceMeta.Resource.Data, resourceMeta.MimeType));
        }
    }
}
