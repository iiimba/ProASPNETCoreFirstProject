using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(long id)
        {
            var product = await _context.Products.Where(p => p.ProductId == id).Include(p => p.Category).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound(id);
            }

            return Ok(new ProductVM
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                DateCreated = product.DateCreated,
                Avaliable = product.Avaliable,
                Category = product.Category != null ? new CategoryVM
                {
                    CategoryId = product.Category.CategoryId,
                    Name = product.Category.Name
                } : null
            });
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();

            return Ok(products.Select(p => new ProductVM
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                DateCreated = p.DateCreated,
                Avaliable = p.Avaliable,
                Category = p.Category != null ? new CategoryVM
                {
                    CategoryId = p.Category.CategoryId,
                    Name = p.Category.Name
                } : null
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel productModel)
        {
            var product = new Product
            {
                Name = productModel.Name,
                Price = productModel.Price,
                DateCreated = productModel.DateCreated,
                Avaliable = productModel.Avaliable,
                CategoryId = productModel.CategoryId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModifyProduct(long id, [FromBody] ProductModel modifiedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(id);
            }

            product.Name = modifiedProduct.Name;
            product.Price = modifiedProduct.Price;
            product.DateCreated = modifiedProduct.DateCreated;
            product.Avaliable = modifiedProduct.Avaliable;
            product.CategoryId = modifiedProduct.CategoryId;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(id);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
