using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class ApiAccountController : ControllerBase
    {
        private SignInManager<IdentityUser> signinManager;
        private UserManager<IdentityUser> userManager;
        private IConfiguration configuration;

        public ApiAccountController(SignInManager<IdentityUser> mgr, UserManager<IdentityUser> usermgr, IConfiguration config)
        {
            signinManager = mgr;
            userManager = usermgr;
            configuration = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Credentials creds)
        {
            var result = await signinManager.PasswordSignInAsync(creds.Username, creds.Password, false, false);
            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signinManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] Credentials creds)
        {
            if (await CheckPassword(creds))
            {
                var handler = new JwtSecurityTokenHandler();
                var secret = Encoding.ASCII.GetBytes(configuration["jwtSecret"]);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, creds.Username) }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = handler.CreateToken(descriptor);

                return Ok(new
                {
                    success = true,
                    token = handler.WriteToken(token)
                });
            }

            return Unauthorized();
        }

        private async Task<bool> CheckPassword(Credentials creds)
        {
            var user = await userManager.FindByNameAsync(creds.Username);
            if (user != null)
            {
                foreach (IPasswordValidator<IdentityUser> v in userManager.PasswordValidators)
                {
                    if ((await v.ValidateAsync(userManager, user, creds.Password)).Succeeded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public class Credentials
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }
    }
}