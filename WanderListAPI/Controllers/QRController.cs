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
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class QRController : ControllerBase
    {
        private readonly WanderListDbContext _context;
        private readonly ILogger _logger;

        public QRController(WanderListDbContext context, ILogger<ResourceMeta> logger)
        {
            _context = context;
            _logger = logger;
        }

        // POST api/<QRController>/5
        // GET api/<apiVersion>/<ApplicationUserController>/5
        [HttpGet("{qrCode}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post(Guid qrCode, [FromBody] Guid userId)
        {
            _logger.LogInformation($"GET QR with id {qrCode}");
            var qr = await _context.QR
                .Where(q => q.QRId == qrCode)
                .FirstOrDefaultAsync();

            // Check QR exists
            if (qr == default(QR))
            {
                return NotFound(new Response()
                {
                    Message = $"No QR code {qrCode}",
                    Status = "404"
                });

                // Check QR is not expired
            } else if (qr.Expiry < DateTime.Now)
            {
                return BadRequest(new Response()
                {
                    Message = $"QR code {qrCode} has expired",
                    Status = "400"
                });
            }

            _logger.LogInformation($"GET Users {userId}");
            var user = await _context.ApplicationUser
                .Where(use => use.Id == userId.ToString())
                .FirstOrDefaultAsync();

            // Check User exists
            if (user == default(AppUser))
            {
                return NotFound(new Response()
                {
                    Message = $"No user exists with id {userId}",
                    Status = "404"
                });
            }

            _logger.LogInformation($"GET History for QR code {qrCode} and User with id {userId}");
            var history = await _context.History
                .Where(hist => hist.UserId == userId.ToString() && hist.ContentId == qr.ContentId)
                .FirstOrDefaultAsync();

            // Check user has not already visited content
            if (history != default(History))
            {
                return BadRequest(new Response()
                {
                    Message = $"User with id {userId} has already been to this location",
                    Status = "400"
                });
            }

            // Add new history
            history = new History()
            {
                ContentId = qr.ContentId,
                Date = DateTime.Now,
                UserId = userId.ToString()
            };
            _context.History.Add(history);

            await _context.SaveChangesAsync();
            return Ok(new Response()
            {
                Message = $"QR code {qrCode} has bee verified",
                Status = "201"
            });
        }
    }
}
