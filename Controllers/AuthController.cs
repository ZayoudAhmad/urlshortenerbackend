using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using urlshortenerbackend.Dto.User;
using urlshortenerbackend.Models;
using urlshortenerbackend.Repositories;

namespace urlshortenerbackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IUserRepository _userRepository;
    private readonly IConfiguration configuration;

    public AuthController(IUserRepository userRepository, IConfiguration configuration)
    {
        this._userRepository = userRepository;
        this.configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(CreateUserDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            return Conflict(new { message = "A user with this email already exists." });

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
            return BadRequest(new { message = "Password must be at least 6 characters long." });


        User user = new User()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        var createdUser = await _userRepository.CreateAsync(user);

        return Ok(createdUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        User? user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
            return BadRequest(new { message = "Invalid email or password" });

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            return BadRequest(new { message = "Invalid email or password" });

        string token = CreateToken(user);

        return Ok(new
        {
            token,
            user = new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            }
        });
    }

    [Authorize]
    [HttpGet]
    public IActionResult AuthorizedMethod()
    {
        return Ok("you are an authorized user");
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    
}
