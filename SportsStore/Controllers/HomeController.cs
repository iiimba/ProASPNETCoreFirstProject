using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository storeRepository;

        public HomeController(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public async Task<ViewResult> Index()
        {
            var products = await storeRepository.GetProductsAsync();

            return View(products);
        }
    }
}
