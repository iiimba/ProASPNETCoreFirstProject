using Microsoft.AspNetCore.Mvc;
using ProASPNETCoreFirstProject.Models;

namespace ProASPNETCoreFirstProject.Controllers
{
    public class ProductController : Controller
    {
        public IDataSource dataSource = new ProductDataSource();

        public ViewResult Index()
        {
            return View(dataSource.Products);
        }
    }
}
