using AutoMapper;
using IISTestApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer", Roles = "Admins")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await _userManager.Users.ToArrayAsync();
            var result = _mapper.Map<UserViewModel[]>(users);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(CreateUserViewModel user)
        {
            var identityUser =_mapper.Map<IdentityUser>(user);

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(UpdateUserViewModel user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id);
            if (identityUser is null)
            {
                return BadRequest();
            }

            identityUser.UserName = user.UserName;
            identityUser.Email = user.Email;

            var result = await _userManager.UpdateAsync(identityUser);
            if (result.Succeeded && !string.IsNullOrEmpty(user.Password))
            {
                await _userManager.RemovePasswordAsync(identityUser);
                await _userManager.AddPasswordAsync(identityUser, user.Password);

                return Ok();
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser != null)
            {
                await _userManager.DeleteAsync(identityUser);

                return Ok();
            }

            return BadRequest();
        }
    }
}
