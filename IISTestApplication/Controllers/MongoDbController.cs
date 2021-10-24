using IISTestApplication.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MongoDbController : ControllerBase
    {
        private readonly IMongoCollection<User> _users;

        public MongoDbController(IMongoCollection<User> users)
        {
            _users = users;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _users.Find(book => true).ToList();

            return Ok(users);
        }

        [HttpGet("FindByName/{name}")]
        public IActionResult FindByName(string name)
        {
            var users = _users.Find(u => u.Name == name).ToList();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            _users.InsertOne(user);

            return Ok(user.Id);
        }

        [HttpPut]
        public IActionResult Update(string id, User user)
        {
            user.Id = id;
            _users.ReplaceOne(u => u.Id == id, user);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove(string id)
        {
            _users.DeleteOne(u => u.Id == id);

            return Ok();
        }
    }
}
