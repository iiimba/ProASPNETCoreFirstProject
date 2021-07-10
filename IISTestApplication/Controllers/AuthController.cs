using IISTestApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginUserAsync(LoginUserViewModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("Token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginUserViewModel user)
        {
            if (await CheckPassword(user))
            {
                var handler = new JwtSecurityTokenHandler();
                var secret = Encoding.ASCII.GetBytes(_configuration["jwtSecret"]);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.UserName) }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = handler.CreateToken(descriptor);

                return Ok($"Bearer {handler.WriteToken(token)}");
            }

            return Unauthorized();
        }

        private async Task<bool> CheckPassword(LoginUserViewModel user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.UserName);
            if (identityUser != null)
            {
                foreach (var v in _userManager.PasswordValidators)
                {
                    if (!(await v.ValidateAsync(_userManager, identityUser, user.Password)).Succeeded)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
