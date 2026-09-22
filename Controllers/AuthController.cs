using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernMeterAPI.Data;
using ModernMeterAPI.Domain;
using ModernMeterAPI.DTOs.Auth;

namespace ModernMeterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ShinuDbContext _context; //database access
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(ShinuDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null)
            {
                return Conflict(new
                {
                    message = "Email already registered."
                });
            }

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                request.Password
            );

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return Created("", new
            {
                message = "User registered successfully.",
                userId = user.Id,
                fullName = user.FullName,
                email = user.Email
            });
        }
    }
}