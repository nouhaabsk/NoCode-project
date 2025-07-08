using AuthApi.Models;
using AuthApi.Data;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserRegisterDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                {
                    _logger.LogWarning($"Tentative d'inscription avec email existant: {userDto.Email}");
                    return Conflict(new { 
                        success = false,
                        message = "Cet email est déjà utilisé" 
                    });
                }

                var user = new User
                {
                    Email = userDto.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    FullName = userDto.FullName,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Nouvel utilisateur enregistré: {user.Email}");

                return Ok(new 
                {
                    success = true,
                    message = "Inscription réussie",
                    user = new { user.Email, user.FullName }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'inscription");
                return StatusCode(500, new 
                {
                    success = false,
                    message = "Une erreur est survenue"
                });
            }
        }

        // NOUVELLE MÉTHODE LOGIN (AJOUTÉE SANS MODIFIER LE RESTE)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                
                if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    _logger.LogWarning($"Tentative de connexion échouée pour: {loginDto.Email}");
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Email ou mot de passe incorrect"
                    });
                }

                // Génération du token JWT (simplifiée sans utiliser User.Id)
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("fullName", user.FullName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), 
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                _logger.LogInformation($"Connexion réussie pour: {user.Email}");

                return Ok(new
                {
                    success = true,
                    message = "Connexion réussie",
                    token = tokenHandler.WriteToken(token),
                    user = new { user.Email, user.FullName }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la connexion");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Une erreur est survenue"
                });
            }
        }

        // Méthode CheckEmail existante (inchangée)
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery][EmailAddress] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest(new { message = "Email requis" });

                var exists = await _context.Users.AnyAsync(u => u.Email == email);
                
                return Ok(new 
                {
                    exists,
                    available = !exists,
                    message = exists ? "Email déjà utilisé" : "Email disponible"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la vérification d'email");
                return StatusCode(500, new 
                {
                    success = false,
                    message = "Erreur de vérification"
                });
            }
        }
    }

    // DTO existant (inchangé)
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
            ErrorMessage = "Le mot de passe doit contenir 8 caractères, une majuscule et un chiffre")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }
    }

    // NOUVEAU DTO POUR LOGIN (AJOUTÉ)
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}