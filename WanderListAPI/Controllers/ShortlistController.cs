using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShortlistController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;
        public ShortlistController(WanderListDbContext context, ILogger<Shortlist> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/<apiVersion>/<ShortlistController>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<Shortlist>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation($"GET Shortlist all");

            return Ok(await _context.Shortlist.ToListAsync());
        }

        // GET: api/<apiVersion>/<ShortlistController>/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Shortlist), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET Shortlists with id {id}");
            var shortlist = await _context.Shortlist
                    .Where(sho => sho.ShortlistId == id)
                    .FirstOrDefaultAsync();

            if (shortlist == default(Shortlist))
            {
                return NotFound(new Response()
                {
                    Message = $"No Shortlist exists with id {id}",
                    Status = "404"
                });
            }

            return Ok(shortlist);
        }

        // POST api/<apiVersion>/<ShortlistController>
        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Shortlist> shortlists)
        {
            _logger.LogInformation($"POST Shortlists");
            if (shortlists.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "No shortlists to add",
                    Status = "400"
                });
            }
            try
            {
                _context.Shortlist.AddRange(shortlists);
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

            //Todo change null to something meaningful
            return CreatedAtAction(nameof(Post), null , shortlists);
        }

        // PUT api/<apiVersion>/<ShortlistController>
        [HttpPut("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Shortlist shortlist)
        {
            _logger.LogInformation($"PUT Shortlist Content with {id}");
            try
            {
                _context.Shortlist.Update(shortlist);
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

        // PUT api/<apiVersion>/<ShortlistController>
        [HttpPut]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(IEnumerable<Shortlist> shortlists)
        {
            _logger.LogInformation($"PUT Shortlists");
            if (shortlists.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "No shortlists to update",
                    Status = "400"
                });
            }
            try
            {
                _context.Shortlist.UpdateRange(shortlists);
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

        // DELETE api/<apiVersion>/<ShortlistController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"DELETE Shortlist with {id}");

            var shortlist = await _context.Shortlist.FindAsync(id);
            if (shortlist == default(Shortlist))
            {
                return NotFound(new Response()
                {
                    Message = $"No reward exists with id {id}",
                    Status = "404"
                });
            }
            try
            {
                _context.Shortlist.Remove(shortlist);
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

        // DELETE api/<apiVersion>/<ShortlistController>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<Guid> shortlistIds)
        {
            _logger.LogInformation($"DELETE Shortlists");
            if (shortlistIds.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "No shortlists to delete",
                    Status = "400"
                });
            }

            List<Shortlist> shortlists = new List<Shortlist>();
            foreach (Guid id in shortlistIds)
            {
                shortlists.Add(await _context.Shortlist.FindAsync(id));
            }

            try
            {
                _context.Shortlist.RemoveRange(shortlists);
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
    }

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ShortlistContent")]
    [ApiController]
    public class ShortlistContentController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public ShortlistContentController(WanderListDbContext context, ILogger<ShortlistContent> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/<apiVersion>/Shortlist/5/Content
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(List<ItemBriefResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"GET content for shortlist with id {id}");
            var shortlistContent = await _context.ShortlistContent
                .Include(scon => scon.Content)
                .ThenInclude(con => con.Item)
                .ThenInclude(ite => ite.CoverImage)
                .Where(scon => scon.ShortlistId == id)
                .OrderBy(scon => scon.Number)
                .Select(scon => new ItemBriefResponse(scon.Content))
                .ToListAsync();

            return Ok(shortlistContent);
        }

        // POST api/<apiVersion>/<ShortlistContentController>
        [HttpPost]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] IEnumerable<ShortlistContent> shortlistContent)
        {
            _logger.LogInformation($"PUT Shortlist Content");
            if (shortlistContent.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "No shortlist content to update",
                    Status = "400"
                });
            }
            try
            {
                _context.ShortlistContent.AddRange(shortlistContent);
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

            //Todo change null to something meaningful
            return CreatedAtAction(nameof(Post), null, shortlistContent);
        }

        // PUT api/<apiVersion>/<ShortlistContentController>
        [HttpPut("{id}")]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] ShortlistContent shortlistContent)
        {
            _logger.LogInformation($"PUT Shortlist Content with {id}");
            try
            {
                _context.ShortlistContent.Update(shortlistContent);
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

        // PUT api/<apiVersion>/<ShortlistController>
        [HttpPut]
        [Authorize]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(IEnumerable<ShortlistContent> shortlistContent)
        {
            _logger.LogInformation($"PUT Shortlist Content");
            if (shortlistContent.Count() == 0)
            {
                return BadRequest(new Response()
                {
                    Message = "No shortlist content to update",
                    Status = "400"
                });
            }
            try
            {
                _context.ShortlistContent.UpdateRange(shortlistContent);
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

        // DELETE api/<apiVersion>/<ShortlistContentController>/5
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogInformation($"DELETE Shortlist Content with {id}");

            var shortlistContent = await _context.ShortlistContent.FindAsync(id);
            if (shortlistContent == default(ShortlistContent))
            {
                return NotFound(new Response()
                {
                    Message = $"No shortlist content exists with id {id}",
                    Status = "404"
                });
            }
            try
            {
                _context.ShortlistContent.Remove(shortlistContent);
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

        // DELETE api/<apiVersion>/<ShortlistContentController>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<Guid> shortlistContentIds)
        {
            _logger.LogInformation($"DELETE Shortlist Content");
            List<ShortlistContent> shortlistContent = new List<ShortlistContent>();
            foreach (Guid id in shortlistContentIds)
            {
                shortlistContent.Add(await _context.ShortlistContent.FindAsync(id));
            }

            try
            {
                _context.ShortlistContent.RemoveRange(shortlistContent);
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
    }
}
