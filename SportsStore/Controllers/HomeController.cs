using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize = 4;

        private readonly IStoreRepository storeRepository;

        public HomeController(IStoreRepository storeRepository)
        {
            this.storeRepository = storeRepository;
        }

        public ViewResult Index(string category, int productPage = 1)
        {
            var productsViewModel = new ProductsListViewModel
            {
                Products = storeRepository.Products
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = storeRepository.Products.Count(p => category == null || p.Category == category)
                },
                CurrentCategory = category
            };

            return View(productsViewModel);
        }
    }
}
