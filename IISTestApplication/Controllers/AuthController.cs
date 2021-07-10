using IISTestApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
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
    }
}
