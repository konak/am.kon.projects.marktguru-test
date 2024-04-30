using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using am.kon.projects.marktguru_test.Models;
using am.kon.projects.marktguru_test.product.business_logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace am.kon.projects.marktguru_test.Controllers.Api;

/// <summary>
/// Controller providing user authentication functionality
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager
    )
    {
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    /// <summary>
    /// Endpoint to be used to check user login credentials and provide
    /// Bearer token for further authorised operations.
    /// </summary>
    /// <param name="model">Instance of <see cref="LoginModel"/> with user credentials.</param>
    /// <returns>Returns Bearer token to be used in API request headers for authentication.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        IdentityUser? user = await _userManager.FindByNameAsync(model.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            string token = GenerateJwtToken(user);

            return Ok(new { token = token });
        }

        return Unauthorized();
    }
    
    /// <summary>
    /// Method generating Bearer Jwt token. 
    /// </summary>
    /// <param name="user">Instance of <see cref="IdentityUser"/> representing current user.</param>
    /// <returns>Returns Bearer Jwt token.</returns>
    private string GenerateJwtToken(IdentityUser user)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

        IList<string> userRoles = _userManager.GetRolesAsync(user).Result;
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}