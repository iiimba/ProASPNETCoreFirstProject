using IISTestApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CorsController : ControllerBase
    {
        [HttpGet("OkJsonResult")]
        public IActionResult GetOkJsonResult()
        {
            var person = new Person { Firstname = "Vlad", Surname = "Mis", PersonId = 1 };

            return Ok(person);
        }

        [HttpGet("String")]
        public string GetString()
        {
            return "Hello World!";
        }

        [HttpPost("OkJsonResult")]
        public IActionResult PostOkJsonResult()
        {
            var person = new Person { Firstname = "Vlad", Surname = "Mis", PersonId = 1 };

            return Ok(person);
        }

        [HttpPost("String")]
        public string PostString()
        {
            return "Hello World!";
        }

        [HttpPut("OkJsonResult")]
        public IActionResult PutOkJsonResult()
        {
            var person = new Person { Firstname = "Vlad", Surname = "Mis", PersonId = 1 };

            return Ok(person);
        }

        [HttpPut("String")]
        public string PutString()
        {
            return "Hello World!";
        }
    }
}
