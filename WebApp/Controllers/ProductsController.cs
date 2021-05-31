using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IAsyncEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductBindingTarget target)
        {
            var p = target.ToProduct();
            await _context.Products.AddAsync(p);
            await _context.SaveChangesAsync();

            return Ok(p);
        }

        [HttpPut]
        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id)
        {
            _context.Products.Remove(new Product() { ProductId = id });
            await _context.SaveChangesAsync();
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            return RedirectToAction(nameof(GetProduct), new { Id = 1 });
        }
    }
}
