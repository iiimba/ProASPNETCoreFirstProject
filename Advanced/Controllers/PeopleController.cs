using Advanced.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Advanced.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer")]
    public class PeopleController : ControllerBase
    {
        private readonly DataContext _context;

        public PeopleController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<Person[]> Get()
        {
            var peoples = await _context.People.Include(p => p.Department).Include(p => p.Location).ToArrayAsync();
            foreach (var people in peoples)
            {
                people.Department.People = null;
                people.Location.People = null;
            }

            return peoples;
        }
    }
}
