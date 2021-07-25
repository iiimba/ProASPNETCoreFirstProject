using IISTestApplication.Models;
using IISTestApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public RedisController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key, CancellationToken token)
        {
            var person = await _redisService.GetAsync<Person>(key, token);

            return Ok(person);
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> Post(string key, Person person, CancellationToken token)
        {
            await _redisService.SetAsync(key, person, token);

            return Ok();
        }
    }
}
