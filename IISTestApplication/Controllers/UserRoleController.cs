using AutoMapper;
using IISTestApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer", Roles = "Admins")]
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserRoleController(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("UsersInRole")]
        public async Task<ActionResult> GetRoleMembersAsync(string roleName)
        {
            var identityUsers = await _userManager.GetUsersInRoleAsync(roleName);
            var result = _mapper.Map<UserViewModel[]>(identityUsers);

            return Ok(result);
        }

        [HttpGet("UserRoles")]
        public async Task<ActionResult> GetUserRolesAsync(string userId)
        {
            var identityRoles = await _userManager.GetRolesAsync(new IdentityUser { Id = userId });

            return Ok(identityRoles);
        }

        [HttpPost]
        public async Task<ActionResult> AssignRoleToUserAsync(string userId, string roleName)
        {
            var roleIdentity = await _roleManager.FindByNameAsync(roleName);
            if (roleIdentity is null)
            {
                return BadRequest();
            }

            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser is null)
            {
                return BadRequest();
            }

            if (await _userManager.IsInRoleAsync(identityUser, roleName))
            {
                return BadRequest();
            }

            await _userManager.AddToRoleAsync(identityUser, roleName);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveUserFromRoleAsync(string userId, string roleName)
        {
            var roleIdentity = await _roleManager.FindByNameAsync(roleName);
            if (roleIdentity is null)
            {
                return BadRequest();
            }

            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser is null)
            {
                return BadRequest();
            }

            await _userManager.RemoveFromRoleAsync(identityUser, roleName);

            return Ok();
        }
    }
}
