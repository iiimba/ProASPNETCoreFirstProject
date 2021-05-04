using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProASPNETCoreFirstProject.Models;
namespace ProASPNETCoreFirstProject.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello world";
        }
    }
}
