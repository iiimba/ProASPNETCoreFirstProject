using AutoMapper;
using IISTestApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [Authorize(Roles = "Admins")]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToArrayAsync();
            var result = _mapper.Map<RoleViewModel[]>(roles);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRolesAsync(CreateRoleViewModel role)
        {
            var identityRole = _mapper.Map<IdentityRole>(role);
            var result = await _roleManager.CreateAsync(identityRole);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            var identityRole = await _roleManager.FindByIdAsync(id);
            if (identityRole is null)
            {
                return BadRequest();
            }

            await _roleManager.DeleteAsync(identityRole);

            return Ok();
        }
    }
}
