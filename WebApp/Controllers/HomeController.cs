using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private DataContext context;
        private IEnumerable<Category> Categories => context.Categories;

        public HomeController(DataContext data)
        {
            context = data;
        }

        public IActionResult Index()
        {
            return View(context.Products.Include(p => p.Category));
        }

        public async Task<IActionResult> Details(long id)
        {
            var p = await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            var model = ViewModelFactory.Details(p);

            return View("ProductEditor", model);
        }

        public IActionResult Create()
        {
            return View("ProductEditor", ViewModelFactory.Create(new Product(), Categories));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ProductId = default;
                product.Category = default;
                context.Products.Add(product);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("ProductEditor", ViewModelFactory.Create(product, Categories));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var p = await context.Products.FindAsync(id);
            var model = ViewModelFactory.Edit(p, Categories);

            return View("ProductEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Category = default;
                context.Products.Update(product);

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View("ProductEditor", ViewModelFactory.Edit(product, Categories));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var model = ViewModelFactory.Delete(await context.Products.FindAsync(id), Categories);

            return View("ProductEditor", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            context.Products.Remove(product);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}