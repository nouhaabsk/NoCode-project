using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthApi.Data;
using System.Security.Claims;

namespace AuthApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetUserProfile()
        {
            var email = User.FindFirst(ClaimTypes.Name)?.Value;

            if (email == null)
                return Unauthorized();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
                return NotFound();

            return Ok(new
            {
                user.Email,
                user.FullName,
                user.CreatedAt
            });
        }
    }
}
