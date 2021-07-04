using IISTestApplication.Models;
using IISTestApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsonController : ControllerBase
    {
        private readonly IBsonService _bsonService;

        public BsonController(IBsonService bsonService)
        {
            _bsonService = bsonService;
        }

        
        [HttpPost("Serialize")]
        public IActionResult Serialize(Person person)
        {
            var result = _bsonService.ToBson(person);

            return Ok(result);
        }

        [HttpPost("Deserialize")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Person))]
        public IActionResult Deserialize(string person)
        {
            var result = _bsonService.FromBson<Person>(person);

            return Ok(result);
        }
    }
}
