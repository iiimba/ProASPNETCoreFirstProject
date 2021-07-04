using IISTestApplication.Models;
using IISTestApplication.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        public async Task<Person[]> Get()
        {
            var peoples = await _peopleRepository.GetAllAsync();

            return peoples;
        }
    }
}
