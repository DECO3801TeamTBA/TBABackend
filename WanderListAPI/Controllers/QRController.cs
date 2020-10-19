using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WanderListAPI.Data;
using WanderListAPI.Models;
using WanderListAPI.Models.Junctions;
using WanderListAPI.Utility.Poco;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WanderListAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QRRequest request)
        {
            _logger.LogInformation($"GET QR with id {request.QRCode}");
            var qr = await _context.QR
                .Where(q => q.QRId == request.QRCode)
                .FirstOrDefaultAsync();

            // Check QR exists
            if (qr == default(QR))
            {
                return NotFound(new Response()
                {
                    Message = $"No QR code {request.QRCode}",
                    Status = "404"
                });

                // Check QR is not expired
            } else if (qr.Expiry < DateTime.Now)
            {
                return BadRequest(new Response()
                {
                    Message = $"QR code {request.QRCode} has expired",
                    Status = "400"
                });
            }

            _logger.LogInformation($"GET Users {request.UserId}");
            var user = await _context.ApplicationUser
                .Where(use => use.Id == request.UserId.ToString())
                .FirstOrDefaultAsync();

            // Check User exists
            if (user == default(AppUser))
            {
                return NotFound(new Response()
                {
                    Message = $"No user exists with id {request.UserId}",
                    Status = "404"
                });
            }

            _logger.LogInformation($"GET History for QR code {request.QRCode} and User with id {request.UserId}");
            var history = await _context.History
                .Where(hist => hist.UserId == request.UserId.ToString() && hist.ContentId == qr.ContentId)
                .FirstOrDefaultAsync();

            // Check user has not already visited content
            if (history != default(History))
            {
                return BadRequest(new Response()
                {
                    Message = $"User with id {request.UserId} has already been to this location",
                    Status = "400"
                });
            }

            // Add new history
            history = new History()
            {
                ContentId = qr.ContentId,
                Date = DateTime.Now,
                UserId = request.UserId.ToString()
            };
            _context.History.Add(history);

            // Update CityUser
            var cityId = await _context.Content
                .Where(con => con.ContentId == qr.ContentId)
                .Select(con => con.CityId)
                .FirstOrDefaultAsync();
            var cityUser = await _context.CityUser
                .Where(cuse => cuse.CityId == cityId)
                .FirstOrDefaultAsync();

            if (cityUser == default(CityUser))
            {
                _context.CityUser.Add(new CityUser()
                {
                    CityId = cityId,
                    UserId = request.UserId.ToString(),
                    Count = 1
                });
            } else
            {
                cityUser.Count++;
            }

            // Update UserRewards
            var newRewards = await _context.Reward
                .Where(rew => rew.CityId == cityId && rew.CountThreshold == cityUser.Count)
                .ToListAsync();
            foreach (Reward reward in newRewards)
            {
                _context.UserReward.Add(new UserReward()
                {
                    UserId = request.UserId.ToString(),
                    RewardId = reward.RewardId
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new Response()
            {
                Message = $"QR code {request.QRCode} has been verified",
                Status = "200"
            });
        }
    }
}
