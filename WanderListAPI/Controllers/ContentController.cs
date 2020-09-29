﻿using System;
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
using WanderListAPI.Utility.Poco;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

        // GET: api/<apiVersion>/<ContentController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<ItemBriefResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Content all");
            var content = await _context.Content
                .Include(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .Select(ite => new ItemBriefResponse(ite))
                .ToListAsync();

            return Ok(content);
        }


        // GET api/<apiVersion>/<ContentController>5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Content), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Content with id {id}");
            var content = await _context.Content
                .Include(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .Where(con => con.ContentId == id)
                .FirstOrDefaultAsync();

            if (content == default(Content))
            {
                return NotFound(new Response()
                {
                    Message = $"No Content exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(content);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Content")]
    [ApiController]
    public class ContentResourceMetaController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ContentResourceMetaController(WanderListDbContext context, ILogger<Content> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Content/5/Resource
        [HttpGet("{id}/Resource")]
        [Authorize]
        [ProducesResponseType(typeof(List<ResourceMeta>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Resource for Content with id {id}");
            var resource = await _context.ContentResourceMeta
                .Include(ires => ires.ResourceMeta)
                .Where(ires => ires.ContentId == id)
                .OrderBy(ires => ires.Number)
                .Select(ires => ires.ResourceMeta)
                .ToListAsync();

            return Ok(resource);
        }
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Content")]
    [ApiController]
    public class ContentHistoryController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ContentHistoryController(WanderListDbContext context, ILogger<Content> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Content/5/History
        [HttpGet("{id}/History")]
        [Authorize]
        [ProducesResponseType(typeof(List<ContentHistoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET History for Content with id {id}");
            var history = await _context.History
                    .Where(hist => hist.ContentId == id)
                    .Select(hist => new ContentHistoryResponse(hist))
                    .ToListAsync();

            return Ok(history);
        }
    }
}
